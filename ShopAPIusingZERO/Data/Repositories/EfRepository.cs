using Zero.EFCoreSpecification;
using Zero.SeedWorks;

namespace ShopAPIusingZERO.Data.Repositories
{
    public class EfRepository<TEntity> : RepositoryBase<TEntity, ApplicationDbContext> where TEntity : Entity, IAggregateRoot
    {
        public EfRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
