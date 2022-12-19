using ShopAPIusingZERO.Domain.UserAggregate;
using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;

namespace ShopAPIusingZERO.Domain.ProductAggregate
{
    public class ProductName : ValueObject
    {
        public string Value { get; }
        public static readonly char[] _notAllowedCharacters = new char[] { '$', '^', '`', '<', '>', '+', '/', '=', '~' };

        private ProductName(string value)
        {
            Value = value;
        }
        public static Result<ProductName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<ProductName>("Name Can't Be Blank.");
            if (value.Length > 100)
                return Result.Failure<ProductName>("Name Is Too Long.");
            if (value.IndexOfAny(_notAllowedCharacters) != -1)
                return Result.Failure<ProductName>("Some special characters are not allowed in the name.");

            return Result.Success(new ProductName(value));
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator string(ProductName name)
        {
            return name.Value;
        }
        public static explicit operator ProductName(string name)
        {
            return Create(name).Value;
        }
    }
}
