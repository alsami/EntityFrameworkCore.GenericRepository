using EntityFrameworkCore.GenericRepository.Samples.Shared.Mappings;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.GenericRepository.Samples.Shared
{
    public class SampleDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // you could also say
            // optionsBuilder.UseSqlServer("MyConnectionString")
            // for instance you could inject your configuration and do it like
            // configuration.GetConnectionString("MyDb");
            optionsBuilder.UseInMemoryDatabase("SampleInMemoryDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMapping());
            modelBuilder.ApplyConfiguration(new AddressMapping());
        }
    }
}