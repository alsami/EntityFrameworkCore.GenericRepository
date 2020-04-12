using System.Collections.Generic;

namespace EntityFramework.GenericRepository.Tests.TestObjects
{
    public class CustomerDto
    {
        public string Name { get; }
        
        public ICollection<AddressDto> Addresses { get; }

        public CustomerDto(string name, ICollection<AddressDto> addresses = null)
        {
            this.Name = name;
            this.Addresses = addresses ?? new List<AddressDto>();
        }
    }
}