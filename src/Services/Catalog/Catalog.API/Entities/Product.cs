using Catalog.API.Entities.ValueObjects;
using FluentResults;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ProductId Id { get; private init; }
        public ProductName Name { get; private init; }
        public ProductCategory Category { get; private init; }
        public ProductSummary Summary { get; private init; }
        public ProductDescription Description { get; private init; }
        public ProductImage ImageFile { get; private init; }
        public ProductPrice Price { get; private init; }

        private Product(ProductId id, ProductName name, ProductCategory category, ProductSummary summary,
            ProductDescription description, ProductImage imageFile, ProductPrice price)
        {
            Id = id;
            Name = name;
            Category = category;
            Summary = summary;
            Description = description;
            ImageFile = imageFile;
            Price = price;
        }

        private Product()
        {
        }

        public static Result<Product> Create(string id, string name, string category, string summary,
            string description, string imageFile, decimal price)
        {
            var idResult = ProductId.Create(id);
            var nameResult = ProductName.Create(name);
            var categoryResult = ProductCategory.Create(category);
            var summaryResult = ProductSummary.Create(summary);
            var descriptionResult = ProductDescription.Create(description);
            var imageResult = ProductImage.Create(imageFile);
            var priceResult = ProductPrice.Create(price);

            var result = Result.Combine(idResult, nameResult, categoryResult, summaryResult, descriptionResult, imageResult, priceResult);
            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }

            var product = new Product(idResult.Value, nameResult.Value, categoryResult.Value, summaryResult.Value,
                descriptionResult.Value, imageResult.Value, priceResult.Value);
            return Result.Ok(product);
        }
    }
}
