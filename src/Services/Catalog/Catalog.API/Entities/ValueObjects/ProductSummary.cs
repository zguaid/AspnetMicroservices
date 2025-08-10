using System.Collections.Generic;
using FluentResults;

namespace Catalog.API.Entities.ValueObjects
{
    public class ProductSummary : ValueObject
    {
        public string Value { get; }
        private ProductSummary(string value) => Value = value;

        public static Result<ProductSummary> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail("ProductSummary should not be empty");
            }
            return Result.Ok(new ProductSummary(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
