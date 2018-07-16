using System;

namespace EntityFrameworkCore.GenericRepository.Samples.WebApi.Dtos
{
    public class AddressDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }
    }
}