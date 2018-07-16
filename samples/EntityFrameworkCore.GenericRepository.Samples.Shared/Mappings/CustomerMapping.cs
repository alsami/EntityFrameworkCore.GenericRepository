using EntityFrameworkCore.GenericRepository.Samples.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.GenericRepository.Samples.Shared.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable($"{nameof(Customer)}s");
            
            builder.HasKey(customer => customer.Id);

            builder.Property(customer => customer.Name)
                .IsRequired();

            builder.HasIndex(customer => customer.Name)
                .IsUnique();
        }
    }
}