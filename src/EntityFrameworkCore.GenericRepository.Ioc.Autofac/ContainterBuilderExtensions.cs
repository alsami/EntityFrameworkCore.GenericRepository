using Autofac;
using EntityFrameworkCore.GenericRepository.Abstractions;

namespace EntityFrameworkCore.GenericRepository.Extensions.Autofac
{
    public static class ContainterBuilderExtensions
    {
        public static ContainerBuilder RegisterGenericRepository<TDbContext>(this ContainerBuilder builder)
            where TDbContext : GenericRepositoryContext
        {
            builder.RegisterGeneric(typeof(EntityRepository<,>))
                .As(typeof(IEntityRepository<,>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(PagingRepository<,>))
                .As(typeof(IPagingRepository<,>))
                .InstancePerDependency();

            builder.RegisterType<TDbContext>()
                .As<GenericRepositoryContext>()
                .InstancePerDependency();

            return builder;
        }
    }
}