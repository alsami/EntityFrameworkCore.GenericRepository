namespace EntityFrameworkCore.GenericRepository.TestInfrastracture
{
    public class AddressDto
    {
        public AddressDto(string city)
        {
            this.City = city;
        }

        public string City { get; }
    }
}