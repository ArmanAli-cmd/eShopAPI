using ShopAPIusingZERO.Domain.ShoppingCartAggregate;

namespace ShopAPIusingZERO.Model
{
    public class OrderResponseModel
    {
        public int OrderId{get; set;}
        public CartItem cartItem { get; set;}
        public int TotalQuantity { get; set;}
        public int TotalPrice { get; set;}
    }
}
