using System.Collections.Generic;
using FluentResults;

namespace Catalog.API.Entities.ValueObjects
{
    public class ProductDescription : ValueObject
    {
        public string Value { get; }
        private ProductDescription(string value) => Value = value;

        public static Result<ProductDescription> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail("ProductDescription should not be empty");
            }
            return Result.Ok(new ProductDescription(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
