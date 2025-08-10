using System.Collections.Generic;
using FluentResults;

namespace Catalog.API.Entities.ValueObjects
{
    public class ProductCategory : ValueObject
    {
        public string Value { get; }
        private ProductCategory(string value) => Value = value;

        public static Result<ProductCategory> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail("ProductCategory should not be empty");
            }
            return Result.Ok(new ProductCategory(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
