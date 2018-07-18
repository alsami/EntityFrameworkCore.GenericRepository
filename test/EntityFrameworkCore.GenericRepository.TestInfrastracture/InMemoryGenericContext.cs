using EntityFrameworkCore.GenericRepository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.GenericRepository.TestInfrastracture
{
    public class InMemoryGenericContext : GenericRepositoryContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryTestDb");
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