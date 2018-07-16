using System;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFrameworkCore.GenericRepository.Samples.Shared.Entities
{
    public class Address : IEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public string City { get; set; }
        
        public string Street { get; set; }

        public string PostalCode { get; set; }

        public virtual Customer Customer { get; set; }

        public Guid CustomerId { get; set; }
    }
}