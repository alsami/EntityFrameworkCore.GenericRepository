namespace EntityFramework.GenericRepository.Tests.TestObjects
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