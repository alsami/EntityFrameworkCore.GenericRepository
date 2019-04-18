using System.Collections.Generic;

namespace EntityFrameworkCore.GenericRepository.TestInfrastracture
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