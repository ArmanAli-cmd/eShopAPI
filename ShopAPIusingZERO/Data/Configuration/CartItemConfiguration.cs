using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAPIusingZERO.Domain.ProductAggregate;
using ShopAPIusingZERO.Domain.ShoppingCartAggregate;
using ShopAPIusingZERO.Domain.UserAggregate;

namespace ShopAPIusingZERO.Data.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(x => x.CartId);
            
            builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);

            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);

            builder.Property(x => x.ProductName);
                
            builder.Property(m => m.Qty);

            builder.Property(m => m.Price);
        }
    }
}
