using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAPIusingZERO.Domain.UserAggregate;

namespace ShopAPIusingZERO.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => UserName.Create(x).Value);

            builder.Property(x => x.Email)
            .HasConversion(m => m == null || m == UserEmail.Empty ? null : m.Value, a => a == null ? null : (UserEmail)a);

            builder.Property(x => x.MobileNumber)
            .HasConversion(m => m == null || m == UserMobile.Empty ? null : m.Value, a => a == null ? null : (UserMobile)a);
        }
    }
}
