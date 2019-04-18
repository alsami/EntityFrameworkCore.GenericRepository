using System;
using Autofac;
using EntityFrameworkCore.GenericRepository.Abstractions;
using EntityFrameworkCore.GenericRepository.Extensions.Autofac;
using EntityFrameworkCore.GenericRepository.TestInfrastracture;
using Xunit;

namespace EntityFrameworkCore.GenericRepository.Ioc.Autofac.Tests
{
    public class ContainerBuilderExtensionsTests
    {
        [Fact]
        public void ContainerBuildExtensions_RegisterGenerics_And_Repository_Resolveable()
        {
            var container = new ContainerBuilder()
                .RegisterGenericRepository<InMemoryGenericContext>()
                .Build();

            Assert.True(container.IsRegistered<IEntityRepository<Customer, Guid>>(), $"{nameof(IEntityRepository<Customer, Guid>)} not registered");
            Assert.True(container.IsRegistered<IPagingRepository<Customer, Guid>>(), $"{nameof(IPagingRepository<Customer, Guid>)} not registered");
            Assert.True(container.IsRegistered<GenericRepositoryContext>(), $"{nameof(GenericRepositoryContext)}> not registered");

            // additionally resolving types
            container.Resolve<IEntityRepository<Customer, Guid>>();
            container.Resolve<IPagingRepository<Customer, Guid>>();
            container.Resolve<GenericRepositoryContext>();
        }
    }
}
