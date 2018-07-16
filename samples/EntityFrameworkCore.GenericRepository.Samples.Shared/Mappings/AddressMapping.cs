using EntityFrameworkCore.GenericRepository.Samples.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.GenericRepository.Samples.Shared.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable($"{nameof(Address)}es");

            builder.HasKey(address => address.Id);

            builder.Property(address => address.City)
                .IsRequired();
            
            builder.Property(address => address.Street)
                .IsRequired();
            
            builder.Property(address => address.PostalCode)
                .IsRequired();

            builder.HasOne(address => address.Customer)
                .WithMany(customer => customer.Addresses)
                .HasForeignKey(address => address.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}