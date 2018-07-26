using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.GenericRepository.Samples.WebApi.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public ICollection<AddressDto> Addresses { get; set; }
    }
}