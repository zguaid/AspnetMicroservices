using System.Collections.Generic;
using FluentResults;

namespace Catalog.API.Entities.ValueObjects
{
    public class ProductName : ValueObject
    {
        public string Value { get; }
        private ProductName(string value) => Value = value;

        public static Result<ProductName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail("ProductName should not be empty");
            }
            return Result.Ok(new ProductName(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
