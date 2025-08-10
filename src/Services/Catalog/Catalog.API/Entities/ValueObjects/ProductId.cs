using System.Collections.Generic;
using FluentResults;

namespace Catalog.API.Entities.ValueObjects
{
    public class ProductId : ValueObject
    {
        public string Value { get; }
        private ProductId(string value) => Value = value;

        public static Result<ProductId> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail("ProductId should not be empty");
            }
            return Result.Ok(new ProductId(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
