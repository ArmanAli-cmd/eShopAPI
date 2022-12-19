using ShopAPIusingZERO.Domain.ProductAggregate;
using Zero.SeedWorks;

namespace ShopAPIusingZERO.Domain.ShoppingCartAggregate
{
    public class CartItem : Entity, IAggregateRoot
    {
        public int CartId { get; set; } 
        public int UserId { get; private set; }
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Qty { get; private set; }
        public int Price { get; private set; }

        public CartItem(int productId, int userId, string productName, int qty, int price) 
        {
            UserId = userId;
            ProductId = productId;
            ProductName = productName;
            Qty = qty;
            Price= price;
        }
    }
}
