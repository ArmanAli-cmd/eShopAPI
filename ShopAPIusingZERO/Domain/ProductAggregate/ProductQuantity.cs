using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ShopAPIusingZERO.Domain.UserAggregate;
using System.Text.RegularExpressions;
using Zero.SeedWorks;
using Zero.SharedKernel.Constants;
using Zero.SharedKernel.Types.Result;

namespace ShopAPIusingZERO.Domain.ProductAggregate
{
    public class ProductQuantity : ValueObject
    {
        public int Value { get; }
        public ProductQuantity(int value)
        {
            Value = value;
        }
        private ProductQuantity() { }
        public static Result<ProductQuantity> Create(int value)
        {   
            if (value<1)
                return Result.Failure<ProductQuantity>("Product Quantity is Invalid"); //result failure if negative value pass

            return Result.Success(new ProductQuantity(value));//if all above conditions are false value passed to the private constructor
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator int(ProductQuantity qty)
        {
            return qty.Value;
        }
        public static explicit operator ProductQuantity(int qty)
        {
            return Create(qty).Value;
        }
    }
}
