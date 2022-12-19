using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;

namespace ShopAPIusingZERO.Domain.ProductAggregate
{
    public class ProductPrice : ValueObject
    {
        public int Value { get; }
        public ProductPrice(int value)
        {
            Value = value;
        }
        private ProductPrice() { }
        public static Result<ProductPrice> Create(int value)
        {
            if (value < 0)
                return Result.Failure<ProductPrice>("Product Price is Invalid"); //result failure if negative value pass

            return Result.Success(new ProductPrice(value));//if all above conditions are false value passed to the private constructor
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator int(ProductPrice price)
        {
            return price.Value;
        }
        public static explicit operator ProductPrice(int price)
        {
            return Create(price).Value;
        }
    }
}
