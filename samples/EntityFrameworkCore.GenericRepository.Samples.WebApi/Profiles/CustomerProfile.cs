using AutoMapper;
using EntityFrameworkCore.GenericRepository.Samples.Shared.Entities;
using EntityFrameworkCore.GenericRepository.Samples.WebApi.Dtos;

namespace EntityFrameworkCore.GenericRepository.Samples.WebApi.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            this.CreateMap<Customer, CustomerDto>()
                .ReverseMap();
        }
    }
}