using System;
using System.Collections.Generic;
using EntityFrameworkCore.GenericRepository.Samples.WebApi.Dtos;

namespace EntityFrameworkCore.GenericRepository.Samples.WebApi.Controllers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public ICollection<AddressDto> Addresses { get; set; }
    }
}