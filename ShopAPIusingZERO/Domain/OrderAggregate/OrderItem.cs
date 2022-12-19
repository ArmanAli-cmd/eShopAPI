using ShopAPIusingZERO.Domain.ProductAggregate;
using Zero.SeedWorks;

namespace ShopAPIusingZERO.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; private set; }
        public String ProductName { get; private set; }
        public int Quantity { get; private set;}
        public int Price { get; private set; }

        public OrderItem(int productId, string productName, int quantity, int price)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
        private OrderItem() { }
    }
}
