# EntityFrameworkCore.GenericRepository

[![NuGet](https://img.shields.io/nuget/dt/CleanCodeLabs.EntityFrameworkCore.GenericRepository.svg)](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository)
[![NuGet](https://img.shields.io/nuget/v/CleanCodeLabs.EntityFrameworkCore.GenericRepository.svg)](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository)
[![NuGet](https://img.shields.io/nuget/vpre/CleanCodeLabs.EntityFrameworkCore.GenericRepository.svg)](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository)
[![Build Status](https://travis-ci.com/cleancodelabs/EntityFrameworkCore.GenericRepository.svg?branch=master)](https://travis-ci.com/cleancodelabs/EntityFrameworkCore.GenericRepository)

This is a cross platform library and generic-repository implementation for [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore), written in `.netstandard 2.0`, that provides most of the functionalities available for the framework in a generic way.

## Installation

This package is available via nuget. You can install it using Visual-Studio-Nuget-Browser or by using the dotnet-cli for your test-project.

```unspecified
dotnet add package CleanCodeLabs.EntityFrameworkCore.GenericRepository
```

If you want to add a specific version of this package

```unspecified
dotnet add package CleanCodeLabs.EntityFrameworkCore.GenericRepository --version 1.0.0
```

For more information please visit the official [dotnet-cli documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-add-package).

## Usage

Assuming we have a model like this

```csharp

public class Address : IEntity<Guid> // or use abstract GuidEntity 
{
    public Guid Id { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string PostalCode { get; set; }

    public virtual Customer Customer { get; set; }

    public Guid CustomerId { get; set; }
}

```

```csharp
public class Customer : IEntity<Guid> // or use abstract GuidEntity 
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
```

We would create a `DbContext` like this

```csharp
public class SampleDbContext : DbContext
{
    // or use dbcontext options
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // you could also say
        // optionsBuilder.UseSqlServer("MyConnectionString")
        // for instance you could inject your configuration and do it like
        // configuration.GetConnectionString("MyDb");
        // or implement the constructor with `DbContextOptions`
        optionsBuilder.UseInMemoryDatabase("SampleInMemoryDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var customerBuilder = modelBuilder.Entity<Customer>();

        customerBuilder.ToTable($"{nameof(Customer)}s");

        customerBuilder.HasKey(customer => customer.Id);

        customerBuilder.Property(customer => customer.Name)
            .IsRequired();

        customerBuilder.HasIndex(customer => customer.Name)
            .IsUnique();

        var addressBuilder = modelBuilder.Entity<Address>();

        addressBuilder.ToTable($"{nameof(Address)}es");

        addressBuilder.HasKey(address => address.Id);

        addressBuilder.Property(address => address.City)
            .IsRequired();

        addressBuilder.Property(address => address.Street)
            .IsRequired();

        addressBuilder.Property(address => address.PostalCode)
            .IsRequired();

        addressBuilder.HasOne(address => address.Customer)
            .WithMany(customer => customer.Addresses)
            .HasForeignKey(address => address.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```
Now we can register our dependencies with `IServiceCollection` like this

```csharp
var serviceProvider = new ServiceCollection()
    .AddDbContext<DbContext, SampleDbContext>(ServiceLifetime.Transient); // or whatever you need here
    // generic registration
    .AddTransient(typeof(IEntityRepository<,>), typeof(EntityRepository<,>))
    .AddTransient(typeof(IPagingRepository<,>), typeof(PagingRepository<,>)))
    .BuildServiceProvider();

var customerRepository = serviceProvider.GetRequiredService<IEntityRepository<Customer, Guid>>();

var google = new Customer
{
    Id = Guid.NewGuid(),
    Name = "Google",
    Addresses = new List<Address> {
        new Address {
            Id = Guid.NewGuid(),
            City = "San Francisco",
            PostalCode = "94043",
            Street = "Silicon Valley area"
        }
    }
};

// adding the customer and saving the changes
customerRepository.Add(customer);
customerRepository.EnsureChanges(); // this returns a bool when modifiedCount > 0

// now find the customer with an expression, also eager-load the addresses

var searchGoogle = customerRepository.Find(customer => customer.Name == "Google", customer => customer.Addresses);

// we now should have customer google with one address included by eager-loading it
```

All apis are available in a synchronous and asynchronous fashion.

For more information on how to use it, please have a look at the [samples](https://github.com/cleancodelabs/EntityFrameworkCore.GenericRepository/tree/master/samples).