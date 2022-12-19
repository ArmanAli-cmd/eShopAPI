using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopAPIusingZERO.Domain.Order;
using ShopAPIusingZERO.Domain.ProductAggregate;
using ShopAPIusingZERO.Domain.ShoppingCartAggregate;
using System.Reflection;
using Zero.EFCoreSpecification;

namespace ShopAPIusingZERO.Data
{
    public class ApplicationDbContext : DbContextBase<ApplicationDbContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options, mediator) { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



    }
}
