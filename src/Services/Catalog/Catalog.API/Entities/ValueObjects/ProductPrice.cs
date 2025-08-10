using System.Collections.Generic;
using FluentResults;

namespace Catalog.API.Entities.ValueObjects
{
    public class ProductPrice : ValueObject
    {
        public decimal Value { get; }
        private ProductPrice(decimal value) => Value = value;

        public static Result<ProductPrice> Create(decimal value)
        {
            if (value < 0)
            {
                return Result.Fail("ProductPrice should be positive");
            }
            return Result.Ok(new ProductPrice(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
