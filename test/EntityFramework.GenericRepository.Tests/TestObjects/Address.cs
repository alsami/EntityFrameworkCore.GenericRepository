using System;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFramework.GenericRepository.Tests.TestObjects
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