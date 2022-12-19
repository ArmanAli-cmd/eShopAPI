using ShopAPIusingZERO.Domain.OrderAggregate;
using Zero.SeedWorks;

namespace ShopAPIusingZERO.Domain.Order
{
    public class Order : Entity, IAggregateRoot
    {
        public int OrderId { get; private set; }
        public int UserId { get; private set; }
        public int TotalQuantity { get; private set; }
        public int TotalPrice { get; private set; }

        private List<OrderItem> _items = new();
        
        public IReadOnlyList<OrderItem> items => _items.AsReadOnly();
        private Order() { }

        public Order(int userId, int totalQuantity, int totalPrice, List<OrderItem> items)
        {
            UserId = userId;
            TotalQuantity = totalQuantity;
            TotalPrice = totalPrice;
            _items = items;
        }
    }
}
