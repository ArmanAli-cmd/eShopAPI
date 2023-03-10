using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAPIusingZERO.Domain.Order;
using ShopAPIusingZERO.Domain.ProductAggregate;
using ShopAPIusingZERO.Domain.UserAggregate;

namespace ShopAPIusingZERO.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);

            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);

            builder.Property(x => x.TotalQuantity);
            builder.Property(x => x.TotalPrice);
            builder.OwnsMany(m => m.items, b => {
                b.HasKey(x => x.OrderItemId);
                b.WithOwner().HasForeignKey(m => m.OrderId);
                b.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);
                b.Property(x => x.ProductName);
                b.Property(m => m.Quantity);
                b.Property(m => m.Price);
            });
        }
    }
}
