using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCore.GenericRepository;
using EntityFrameworkCore.GenericRepository.Abstractions;
using EntityFrameworkCore.GenericRepository.Shared;
using EntityFrameworkCore.GenericRepository.TestInfrastracture;
using Xunit;

namespace EntityFramework.GenericRepository.Tests
{
    public class PagingRepositoryTests
    {
        private readonly IPagingRepository<Customer, Guid> customersPagingRepository;
        private readonly IEntityRepository<Customer, Guid> customersRepository;
        private const string SomeEntityQuantityPropertyName = "CustomerNumber";

        public PagingRepositoryTests()
        {
            this.customersPagingRepository = new PagingRepository<Customer, Guid>(new InMemoryGenericContext());
            this.customersRepository = new EntityRepository<Customer, Guid>(new InMemoryGenericContext());
        }

        [Fact]
        public async Task PagingRepository_AddMultipleWithChildrenAndReadPagedAsync_ExpectSuccess()
        {

            for (var i = 0; i < 100; i++)
            {
                var createCustomer = GetNewCustomer();

                createCustomer.Addresses = GetNewAddresses(20, createCustomer).ToList();

                this.customersRepository.Add(createCustomer);
                
                var added = await this.customersRepository.EnsureChangesAsync();
            }

            for (var i = 1; i <= 10; i++)
            {
                var pagedSet = await this.customersPagingRepository.GetPagedAsync(i, 10, PagingRepositoryTests.SomeEntityQuantityPropertyName);

                Assert.NotNull(pagedSet);
                Assert.Equal(10, pagedSet.Entities.Count());

                var entities = pagedSet.Entities.ToArray();

                for (var j = 1; j < entities.Length; j++)
                {
                    Assert.True(entities[j - 1].CustomerNumber <= entities[j].CustomerNumber);
                }

                pagedSet = await this.customersPagingRepository.GetPagedAsync(i, 10, PagingRepositoryTests.SomeEntityQuantityPropertyName,
                    SortDirection.Descending);

                Assert.NotNull(pagedSet);
                Assert.Equal(10, pagedSet.Entities.Count());

                entities = pagedSet.Entities.ToArray();

                for (var j = 1; j < entities.Length; j++)
                {
                    Assert.True(entities[j - 1].CustomerNumber >= entities[j].CustomerNumber);
                }

                pagedSet = await this.customersPagingRepository.GetPagedAsync(i, 10, PagingRepositoryTests.SomeEntityQuantityPropertyName,
                    SortDirection.Descending, someEntity => someEntity.CustomerNumber >= 120);

                Assert.NotNull(pagedSet);
                Assert.True(pagedSet.Entities.Count() <= 10);
                Assert.True(pagedSet.Entities.All(someEntity => someEntity.CustomerNumber >= 120));

                entities = pagedSet.Entities.ToArray();

                for (var j = 1; j < entities.Length; j++)
                {
                    Assert.True(entities[j - 1].CustomerNumber >= entities[j].CustomerNumber);
                }

                pagedSet = await this.customersPagingRepository.GetPagedAsync(i, 10, PagingRepositoryTests.SomeEntityQuantityPropertyName,
                    SortDirection.Ascending, someEntity => someEntity.CustomerNumber <= 119);

                Assert.NotNull(pagedSet);
                Assert.True(pagedSet.Entities.Count() <= 10);
                Assert.True(pagedSet.Entities.All(someEntity => someEntity.CustomerNumber <= 119));

                entities = pagedSet.Entities.ToArray();

                for (var j = 1; j < entities.Length; j++)
                {
                    Assert.True(entities[j - 1].CustomerNumber <= entities[j].CustomerNumber);
                }

                pagedSet = await this.customersPagingRepository.GetPagedAsync(i, 5, PagingRepositoryTests.SomeEntityQuantityPropertyName,
                    SortDirection.Descending, someEntity => someEntity.CustomerNumber >= 120,
                    someEntity => someEntity.Addresses);

                Assert.NotNull(pagedSet);
                Assert.True(pagedSet.Entities.Count() <= 5);
                Assert.True(pagedSet.Entities.All(someEntity => someEntity.CustomerNumber >= 120));

                entities = pagedSet.Entities.ToArray();

                for (var j = 1; j < entities.Length; j++)
                {
                    Assert.True(entities[j - 1].CustomerNumber >= entities[j].CustomerNumber);
                }

                pagedSet = await this.customersPagingRepository.GetPagedAsync(i, 7, PagingRepositoryTests.SomeEntityQuantityPropertyName,
                    SortDirection.Ascending, someEntity => someEntity.Addresses);

                Assert.NotNull(pagedSet);
                Assert.Equal(7, pagedSet.Entities.Count());

                entities = pagedSet.Entities.ToArray();

                for (var j = 1; j < entities.Length; j++)
                {
                    Assert.True(entities[j - 1].CustomerNumber <= entities[j].CustomerNumber);
                }
            }
        }

        private static Customer GetNewCustomer()
        {
            var random = new Random();
            return new Customer
            {
                Id = Guid.NewGuid(),
                Name = (random.Next(10000, 99999) + DateTime.UtcNow.Ticks).ToString(),
                CustomerNumber = random.Next(10000, 99999)
            };
        }

        private static IEnumerable<Address> GetNewAddresses(int quantity, Customer someEntity)
        {
            for (var i = 0; i < quantity; i++)
            {
                yield return new Address
                {
                    CustomerId = someEntity.Id,
                    Id = Guid.NewGuid()
                };
            }
        }
    }
}