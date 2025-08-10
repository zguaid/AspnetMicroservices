using Catalog.API.Entities;
using Catalog.API.Entities.ValueObjects;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        static CatalogContext()
        {
            BsonSerializer.RegisterSerializer(new ProductIdSerializer());
            BsonSerializer.RegisterSerializer(new StringValueObjectSerializer<ProductName>(ProductName.Create, x => x.Value));
            BsonSerializer.RegisterSerializer(new StringValueObjectSerializer<ProductCategory>(ProductCategory.Create, x => x.Value));
            BsonSerializer.RegisterSerializer(new StringValueObjectSerializer<ProductSummary>(ProductSummary.Create, x => x.Value));
            BsonSerializer.RegisterSerializer(new StringValueObjectSerializer<ProductDescription>(ProductDescription.Create, x => x.Value));
            BsonSerializer.RegisterSerializer(new StringValueObjectSerializer<ProductImage>(ProductImage.Create, x => x.Value));
            BsonSerializer.RegisterSerializer(new DecimalValueObjectSerializer<ProductPrice>(ProductPrice.Create, x => x.Value));
        }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
    }
}
