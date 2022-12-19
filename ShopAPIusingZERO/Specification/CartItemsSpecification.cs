using ShopAPIusingZERO.Domain.ShoppingCartAggregate;
using Zero.SeedWorks;

namespace ShopAPIusingZERO.Specification
{
    public class CartItemsSpecification : BaseSpecification<CartItem>
    {
        public CartItemsSpecification(int id) : base(m => m.UserId==id) { }
    }
}
