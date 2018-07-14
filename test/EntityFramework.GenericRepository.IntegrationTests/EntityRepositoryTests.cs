using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using EntityFrameworkCore.GenericRepository;
using EntityFrameworkCore.GenericRepository.Abstractions;
using EntityFrameworkCore.GenericRepository.TestInfrastracture;
using Xunit;

namespace EntityFramework.GenericRepository.IntegrationTests
{
    public class EntityRepositoryTests
    {
        private readonly IEntityRepository<Customer, Guid> repository;

        private readonly string[] cities =
        {
            "Amsterdam",
            "Berlin",
            "New York",
            "Mumbai",
            "Damaskus",
            "London",
            "Paris",
            "Rom"
        };
        
        private readonly string[] customerNames =
        {
            "Apple",
            "Microsoft",
            "Mentorgraphics",
            "Amazon",
            "DHL",
            "Random Company",
        };

        public EntityRepositoryTests()
        {
            this.repository = new EntityRepository<Customer, Guid>(new InMemoryGenericContext());
        }

        [Fact]
        public async Task GenericEntityRepository_CreateReadUpdateDeleteMultipleAsync_ExpectSuccess()
        {
            var createCustomers = this.CreateRandom(5, 3);

            foreach (var createCustomer in createCustomers)
            {
                await this.repository.AddAsync(createCustomer);
                var added = await this.repository.EnsureChangesAsync();
                Assert.True(added);
            }

            var createIds = createCustomers.Select(customer => customer.Id).ToArray();

            foreach (var id in createIds)
            {
                var existingCustomer =
                    await this.repository.FindAsync(customer => customer.Id == id, customer => customer.Addresses);

                Assert.NotNull(existingCustomer);
                Assert.True(existingCustomer.Addresses.Count == 3);

                var newName = $"NewName_{id}";

                existingCustomer.Name = newName;

                this.repository.Edit(existingCustomer);
                var updated = await this.repository.EnsureChangesAsync();
                Assert.True(updated);

                existingCustomer =
                    await this.repository.FindAsync(customer => customer.Id == id, customer => customer.Addresses);

                Assert.Equal(newName, existingCustomer.Name);
            }

            var allCustomers = await this.repository.GetAllAsync();

            Assert.True(createIds.All(createId => allCustomers.Select(customer => customer.Id).Contains(createId)));

            allCustomers = await this.repository.FindAllAsync(customer => customer.Name.StartsWith("NewName"),
                customer => customer.Addresses);

            var customersArray = allCustomers as Customer[] ?? allCustomers.ToArray();

            Assert.True(createCustomers.Select(customer => customer.Id).All(createId =>
                customersArray.Select(customer => customer.Id).Contains(createId)));

            var intersectingIds = createCustomers.Select(customer => customer.Id)
                .Intersect(customersArray.Select(customer => customer.Id));

            foreach (var id in intersectingIds)
            {
                var customer = customersArray.First(list => list.Id == id);

                Assert.True(customer.Addresses.Count == 3);
            }

            foreach (var id in createIds)
            {
                var existingCustomer = await this.repository.FindAsync(id);

                Assert.NotNull(existingCustomer);

                existingCustomer = await this.repository.FindAsync(customer => customer.Name == $"NewName_{id}");

                Assert.NotNull(existingCustomer);

                this.repository.Delete(existingCustomer);

                var deleted = await this.repository.EnsureChangesAsync();

                Assert.True(deleted);
            }

            allCustomers = await this.repository.GetAllAsync();

            customersArray = allCustomers as Customer[] ?? allCustomers.ToArray();

            foreach (var allId in customersArray.Select(customer => customer.Id))
            {
                Assert.DoesNotContain(createIds, id => id == allId);
            }
        }

        [Fact]
        public void GenericEntityRepository_CreateReadUpdateDeleteMultiple_ExpectSuccess()
        {
            var createCustomers = this.CreateRandom(5, 3);

            foreach (var createCustomer in createCustomers)
            {
                this.repository.Add(createCustomer);
                var added = this.repository.EnsureChanges();
                Assert.True(added);
            }

            var createIds = createCustomers.Select(customer => customer.Id).ToArray();

            foreach (var id in createIds)
            {
                var existingCustomer = this.repository.Find(customer => customer.Id == id, customer => customer.Addresses);

                Assert.NotNull(existingCustomer);
                Assert.True(existingCustomer.Addresses.Count == 3);

                var newName = $"NewName_{id}";

                existingCustomer.Name = newName;

                this.repository.Edit(existingCustomer);
                var updated = this.repository.EnsureChanges();
                Assert.True(updated);

                existingCustomer = this.repository.Find(customer => customer.Id == id, customer => customer.Addresses);

                Assert.Equal(newName, existingCustomer.Name);
            }

            var allCustomers = this.repository.GetAll();

            Assert.True(createCustomers.Select(customer => customer.Id).All(createId =>
                allCustomers.Select(customer => customer.Id).Contains(createId)));

            allCustomers = this.repository.FindAll(customer => customer.Name.StartsWith("NewName"),
                customer => customer.Addresses);

            var customersArray = allCustomers as Customer[] ?? allCustomers.ToArray();

            Assert.True(createCustomers.Select(customer => customer.Id).All(createId =>
                customersArray.Select(customer => customer.Id).Contains(createId)));

            var intersectingIds = createCustomers.Select(customer => customer.Id)
                .Intersect(customersArray.Select(customer => customer.Id));

            foreach (var id in intersectingIds)
            {
                var existingCustomer = customersArray.First(customer => customer.Id == id);

                Assert.True(existingCustomer.Addresses.Count == 3);
            }

            foreach (var id in createIds)
            {
                var existingCustomer = this.repository.Find(id);

                Assert.NotNull(existingCustomer);

                existingCustomer = this.repository.Find(customer => customer.Name == $"NewName_{id}");

                Assert.NotNull(existingCustomer);

                this.repository.Delete(existingCustomer);

                var deleted = this.repository.EnsureChanges();

                Assert.True(deleted);
            }

            allCustomers = this.repository.GetAll();

            customersArray = allCustomers as Customer[] ?? allCustomers.ToArray();

            foreach (var allId in customersArray.Select(customer => customer.Id))
            {
                Assert.DoesNotContain(createIds, id => id == allId);
            }
        }

        [Fact]
        public async Task GenericEntityRepositoryTests_CreateCountMultipleAsync_ExpectSuccess()
        {
            var createCustomers = this.CreateRandom(5);

            foreach (var customer in createCustomers)
            {
                await this.repository.AddAsync(customer);
                var added = await this.repository.EnsureChangesAsync();
                Assert.True(added);
            }

            var count = await this.repository.CountAsync();

            Assert.True(5 <= count);
        }

        [Fact]
        public void GenericEntityRepositoryTests_CreateCountMultiple_ExpectSuccess()
        {
            var createCustomers = this.CreateRandom(5);

            foreach (var createCustomer in createCustomers)
            {
                this.repository.Add(createCustomer);
                var added = this.repository.EnsureChanges();
                Assert.True(added);
            }

            var count = this.repository.Count();

            Assert.True(5 <= count);
        }

        private ICollection<Customer> CreateRandom(int customersCount = 1, int children = 0)
        {
            var random = new Random();

            var customers = new List<Customer>();

            for (var i = 0; i < customersCount; i++)
            {
                var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = this.customerNames[random.Next(0, this.customerNames.Length - 1)]
                };

                for (var j = 0; j < children; j++)
                {
                    customer.Addresses.Add(this.CreateRandom(customer.Id));
                }

                customers.Add(customer);
            }

            return customers;
        }

        private Address CreateRandom(Guid customerId)
        {
            var random = new Random();
            return new Address
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                City = this.cities[random.Next(0, this.cities.Length - 1)],
                PostalCode = random.Next(1111, 9999).ToString(),
                Street = "abcdfgh"
                
            };
        }
    }
}