using System.Collections.Generic;
using FluentResults;

namespace Catalog.API.Entities.ValueObjects
{
    public class ProductImage : ValueObject
    {
        public string Value { get; }
        private ProductImage(string value) => Value = value;

        public static Result<ProductImage> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail("ProductImage should not be empty");
            }
            return Result.Ok(new ProductImage(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
