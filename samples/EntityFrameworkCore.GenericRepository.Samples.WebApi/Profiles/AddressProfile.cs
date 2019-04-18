using AutoMapper;
using AutoMapper.EquivalencyExpression;
using EntityFrameworkCore.GenericRepository.Samples.Shared.Entities;
using EntityFrameworkCore.GenericRepository.Samples.WebApi.Dtos;

namespace EntityFrameworkCore.GenericRepository.Samples.WebApi.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            this.CreateMap<Address, AddressDto>()
                .ReverseMap()
                .EqualityComparison((dto, entity) => dto.Id == entity.Id);
        }
    }
}