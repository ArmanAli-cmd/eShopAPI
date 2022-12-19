using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAPIusingZERO.Domain.Order;
using ShopAPIusingZERO.Domain.OrderAggregate;
using ShopAPIusingZERO.Domain.ProductAggregate;

namespace ShopAPIusingZERO.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.OrderItemId);

            builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);

            builder.Property(x => x.ProductName);
            builder.Property(m => m.Quantity);

            builder.Property(m => m.Price);
        }
    }
}
