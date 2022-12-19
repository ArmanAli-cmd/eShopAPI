using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAPIusingZERO.Domain.ProductAggregate;
using System;

namespace ShopAPIusingZERO.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) {
            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.ProductName)
                .HasConversion(x => x.Value, x => ProductName.Create(x).Value);

            builder.Property(m => m.Quantity)
            .HasConversion(x => x.Value, x => ProductQuantity.Create(x).Value);

            builder.Property(m => m.Price)
                .HasConversion(x => x.Value, x => ProductPrice.Create(x).Value);
        }
    }
}
