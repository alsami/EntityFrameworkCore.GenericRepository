using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCore.GenericRepository;
using EntityFrameworkCore.GenericRepository.Abstractions;
using EntityFrameworkCore.GenericRepository.TestInfrastracture;
using Xunit;

namespace EntityFramework.GenericRepository.Tests
{
    public class EntityRepositoryTests
    {
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

        [Fact]
        public async Task GenericEntityRepository_CreateReadUpdateDeleteMultipleAsync_ExpectSuccess()
        {
            var createCustomers = this.CreateRandom(5, 3);

            var repository = CreateRepository();

            foreach (var createCustomer in createCustomers)
            {
                await repository.AddAsync(createCustomer);
                var added = await repository.EnsureChangesAsync();
                Assert.True(added);
            }

            var createIds = createCustomers.Select(customer => customer.Id).ToArray();

            foreach (var id in createIds)
            {
                var existingCustomer =
                    await repository.FindAsync(customer => customer.Id == id, customer => customer.Addresses);

                Assert.NotNull(existingCustomer);
                Assert.True(existingCustomer.Addresses.Count == 3);

                var newName = $"NewName_{id}";

                existingCustomer.Name = newName;

                repository.Edit(existingCustomer);
                var updated = await repository.EnsureChangesAsync();
                Assert.True(updated);

                existingCustomer =
                    await repository.FindAsync(customer => customer.Id == id, customer => customer.Addresses);

                Assert.Equal(newName, existingCustomer.Name);

                var hasMatchingByNewName = await repository.HasMatchingAsync(customer => customer.Name == newName);
                
                Assert.True(hasMatchingByNewName);
            }

            var allCustomers = await repository.GetAllAsync();

            Assert.True(createIds.All(createId => allCustomers.Select(customer => customer.Id).Contains(createId)));

            allCustomers = await repository.FindAllAsync(customer => customer.Name.StartsWith("NewName"),
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
                var existingCustomer = await repository.FindAsync(id);

                Assert.NotNull(existingCustomer);

                existingCustomer = await repository.FindAsync(customer => customer.Name == $"NewName_{id}");

                Assert.NotNull(existingCustomer);

                repository.Delete(existingCustomer);

                var deleted = await repository.EnsureChangesAsync();

                Assert.True(deleted);
            }

            allCustomers = await repository.GetAllAsync();

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
            
            var repository = CreateRepository();

            foreach (var createCustomer in createCustomers)
            {
                repository.Add(createCustomer);
                var added = repository.EnsureChanges();
                Assert.True(added);
            }

            var createIds = createCustomers.Select(customer => customer.Id).ToArray();

            foreach (var id in createIds)
            {
                var existingCustomer = repository.Find(customer => customer.Id == id, customer => customer.Addresses);

                Assert.NotNull(existingCustomer);
                Assert.True(existingCustomer.Addresses.Count == 3);

                var newName = $"NewName_{id}";

                existingCustomer.Name = newName;

                repository.Edit(existingCustomer);
                var updated = repository.EnsureChanges();
                Assert.True(updated);

                existingCustomer = repository.Find(customer => customer.Id == id, customer => customer.Addresses);

                Assert.Equal(newName, existingCustomer.Name);

                var hasMatchingByNewName = repository.HasMatching(customer => customer.Name == newName);
                
                Assert.True(hasMatchingByNewName);
            }

            var allCustomers = repository.GetAll();

            Assert.True(createCustomers.Select(customer => customer.Id).All(createId =>
                allCustomers.Select(customer => customer.Id).Contains(createId)));

            allCustomers = repository.FindAll(customer => customer.Name.StartsWith("NewName"),
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
                var existingCustomer = repository.Find(id);

                Assert.NotNull(existingCustomer);

                existingCustomer = repository.Find(customer => customer.Name == $"NewName_{id}");

                Assert.NotNull(existingCustomer);

                repository.Delete(existingCustomer);

                var deleted = repository.EnsureChanges();

                Assert.True(deleted);
            }

            allCustomers = repository.GetAll();

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
            
            var repository = CreateRepository();

            foreach (var customer in createCustomers)
            {
                await repository.AddAsync(customer);
                var added = await repository.EnsureChangesAsync();
                Assert.True(added);
            }

            var count = await repository.CountAsync();

            Assert.Equal(5, count);
        }

        [Fact]
        public void GenericEntityRepositoryTests_CreateCountMultiple_ExpectSuccess()
        {
            var createCustomers = this.CreateRandom(5);
            
            var repository = CreateRepository();

            foreach (var createCustomer in createCustomers)
            {
                repository.Add(createCustomer);
                var added = repository.EnsureChanges();
                Assert.True(added);
            }

            var count = repository.Count();

            Assert.Equal(5, count);
        }

        [Fact]
        public void GenericRepositoryTests_AddMany_Expect_Amount_Added()
        {
            var createCustomer = this.CreateRandom(100);
            var repository = CreateRepository();
            
            repository.AddMany(createCustomer);
            repository.EnsureChanges();
            
            Assert.Equal(100, repository.Count());
        }
        
        [Fact]
        public async Task GenericRepositoryTests_AddManyAsync_Expect_Amount_Added()
        {
            var createCustomer = this.CreateRandom(100);
            var repository = CreateRepository();
            
            await repository.AddManyAsync(createCustomer);
            await repository.EnsureChangesAsync();
            
            Assert.Equal(100, await repository.CountAsync());
        }

        [Fact]
        public void GenericEntityRepositoryTests_GetAllProjected_Expect_Projection()
        {
            var createCustomers = this.CreateRandom(5);
            var repository = CreateRepository();
            repository.AddMany(createCustomers);
            repository.EnsureChanges();

            var projected = repository.GetAll(customer 
                => new CustomerDto(customer.Name, customer.Addresses.Select(address => new AddressDto(address.City)).ToArray()));
            
            var customerDtos = projected as CustomerDto[] ?? projected.ToArray();
            
            Assert.NotEmpty(customerDtos);
            Assert.Equal(5, customerDtos.Count());
            Assert.Equal(createCustomers.OrderBy(customer => customer.Name).Select(customer => customer.Name), customerDtos.OrderBy(customerDto => customerDto.Name).Select(x => x.Name));
        }
        
        [Fact]
        public async Task GenericEntityRepositoryTests_GetAllProjectedAsync_Expect_Projection()
        {
            var createCustomers = this.CreateRandom(5);
            var repository = CreateRepository();
            await repository.AddManyAsync(createCustomers);
            await repository.EnsureChangesAsync();

            var projected = await repository.GetAllAsync(customer 
                => new CustomerDto(customer.Name, customer.Addresses.Select(address => new AddressDto(address.City)).ToArray()));
            
            var customerDtos = projected as CustomerDto[] ?? projected.ToArray();
            
            Assert.NotEmpty(customerDtos);
            Assert.Equal(5, customerDtos.Count());
            Assert.Equal(createCustomers.OrderBy(customer => customer.Name).Select(customer => customer.Name), customerDtos.OrderBy(customerDto => customerDto.Name).Select(x => x.Name));
        }
        
        [Fact]
        public void GenericEntityRepositoryTests_GetAllProjected_Includes_Expect_Projection()
        {
            var createCustomers = this.CreateRandom(5, 10);
            var repository = CreateRepository();
            repository.AddMany(createCustomers);
            repository.EnsureChanges();

            var projected = repository.GetAll(customer 
                => new CustomerDto(customer.Name, customer.Addresses.Select(address => new AddressDto(address.City)).ToArray()), customer => customer.Addresses);
            
            var customerDtos = projected as CustomerDto[] ?? projected.ToArray();
            
            Assert.NotEmpty(customerDtos);
            foreach (var customerDto in customerDtos)
            {
                Assert.Equal(10, customerDto.Addresses.Count);
            }
            Assert.Equal(5, customerDtos.Count());
            Assert.Equal(createCustomers.OrderBy(customer => customer.Name).Select(customer => customer.Name), customerDtos.OrderBy(customerDto => customerDto.Name).Select(x => x.Name));
        }
        
        [Fact]
        public async Task GenericEntityRepositoryTests_GetAllProjectedAsync_Includes_Expect_Projection()
        {
            var createCustomers = this.CreateRandom(5, 10);
            var repository = CreateRepository();
            await repository.AddManyAsync(createCustomers);
            await repository.EnsureChangesAsync();

            var projected = await repository.GetAllAsync(customer 
                => new CustomerDto(customer.Name, customer.Addresses.Select(address => new AddressDto(address.City)).ToArray()));
            
            var customerDtos = projected as CustomerDto[] ?? projected.ToArray();
            
            Assert.NotEmpty(customerDtos);
            foreach (var customerDto in customerDtos)
            {
                Assert.Equal(10, customerDto.Addresses.Count);
            }
            Assert.Equal(5, customerDtos.Count());
            Assert.Equal(createCustomers.OrderBy(customer => customer.Name).Select(customer => customer.Name), customerDtos.OrderBy(customerDto => customerDto.Name).Select(x => x.Name));
        }
        
        [Fact]
        public void GenericEntityRepositoryTests_FindAll_With_Addresses_Expect_Projection()
        {
            var createCustomers = this.CreateRandom(5, 10).Concat(this.CreateRandom(10)).ToArray();
            var repository = CreateRepository();
            repository.AddMany(createCustomers);
            repository.EnsureChanges();

            var projected = repository.FindAll(customer 
                => new CustomerDto(customer.Name, customer.Addresses.Select(address => new AddressDto(address.City)).ToArray()), 
                customer => customer.Addresses.Any());
            
            var customerDtos = projected as CustomerDto[] ?? projected.ToArray();
            
            Assert.NotEmpty(customerDtos);
            Assert.Equal(5, customerDtos.Count());
            foreach (var customerDto in customerDtos)
            {
                Assert.Equal(10, customerDto.Addresses.Count);
            }
            Assert.Equal(5, customerDtos.Count());
        }
        
        [Fact]
        public async Task GenericEntityRepositoryTests_FindAllAsync_With_Addresses_Expect_Projection()
        {
            var createCustomers = this.CreateRandom(5, 10).Concat(this.CreateRandom(10)).ToArray();
            var repository = CreateRepository();
            await repository.AddManyAsync(createCustomers);
            await repository.EnsureChangesAsync();

            var projected = await repository.FindAllAsync(customer 
                => new CustomerDto(customer.Name, customer.Addresses.Select(address => new AddressDto(address.City)).ToArray()), 
                customer => customer.Addresses.Any());
            
            var customerDtos = projected as CustomerDto[] ?? projected.ToArray();
            
            Assert.NotEmpty(customerDtos);
            Assert.Equal(5, customerDtos.Count());
            foreach (var customerDto in customerDtos)
            {
                Assert.Equal(10, customerDto.Addresses.Count);
            }
            Assert.Equal(5, customerDtos.Count());
        }
        
        //
        
        [Fact]
        public void GenericEntityRepositoryTests_FindAll_With_Addresses_Includes_Expect_Projection()
        {
            var createCustomers = this.CreateRandom(5, 10).Concat(this.CreateRandom(10)).ToArray();
            var repository = CreateRepository();
            repository.AddMany(createCustomers);
            repository.EnsureChanges();

            var projected = repository.FindAll(customer 
                => new CustomerDto(customer.Name, customer.Addresses.Select(address => new AddressDto(address.City)).ToArray()), 
                customer => customer.Addresses.Any(), customer => customer.Addresses);
            
            var customerDtos = projected as CustomerDto[] ?? projected.ToArray();
            
            Assert.NotEmpty(customerDtos);
            Assert.Equal(5, customerDtos.Count());
            foreach (var customerDto in customerDtos)
            {
                Assert.Equal(10, customerDto.Addresses.Count);
            }
            Assert.Equal(5, customerDtos.Count());
        }
        
        [Fact]
        public async Task GenericEntityRepositoryTests_FindAllAsync_With_Addresses_Includes_Expect_Projection()
        {
            var createCustomers = this.CreateRandom(5, 10).Concat(this.CreateRandom(10)).ToArray();
            var repository = CreateRepository();
            await repository.AddManyAsync(createCustomers);
            await repository.EnsureChangesAsync();

            var projected = await repository.FindAllAsync(customer 
                => new CustomerDto(customer.Name, customer.Addresses.Select(address => new AddressDto(address.City)).ToArray()), 
                customer => customer.Addresses.Any(), customer => customer.Addresses);
            
            var customerDtos = projected as CustomerDto[] ?? projected.ToArray();
            
            Assert.NotEmpty(customerDtos);
            Assert.Equal(5, customerDtos.Count());
            foreach (var customerDto in customerDtos)
            {
                Assert.Equal(10, customerDto.Addresses.Count);
            }
            Assert.Equal(5, customerDtos.Count());
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
                PostalCode = random.Next(1, 999999).ToString(),
                Street = "abcdfgh"
            };
        }
        
        private static IEntityRepository<Customer, Guid> CreateRepository()
            => new EntityRepository<Customer, Guid>(new InMemoryGenericContext());
    }
}