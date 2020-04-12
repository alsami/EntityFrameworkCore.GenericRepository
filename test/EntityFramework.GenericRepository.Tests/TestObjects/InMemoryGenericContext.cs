using System;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.GenericRepository.Tests.TestObjects
{
    public class InMemoryGenericContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase($"InMemoryTestDb{DateTime.UtcNow.Ticks}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            {
                var ebuilder = builder.Entity<Customer>();

                ebuilder.HasKey(customer => customer.Id);

                ebuilder.HasIndex(customer => customer.Name)
                    .IsUnique();
            }
            
            {
                var ebuilder = builder.Entity<Address>();

                ebuilder.HasKey(address => address.Id);

                ebuilder.HasOne<Customer>()
                    .WithMany(customer => customer.Addresses)
                    .HasForeignKey(address => address.CustomerId);
            }
        }
    }
}