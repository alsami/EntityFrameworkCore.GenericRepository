using System;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFrameworkCore.GenericRepository.TestInfrastracture
{
    public class Address : IEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public Guid CustomerId { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }
    }
}