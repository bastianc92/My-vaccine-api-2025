using AutoMapper;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Configurations.AutoMapperProfiles
{
    public class FamilyGroupMapping : Profile
    {
        public FamilyGroupMapping()
        {
            CreateMap <FamilyGroup, FamilyGroupRequestDto>().ReverseMap();
            CreateMap <FamilyGroup,FamilyGroupResponseDto>()
                .ForMember(dest => dest.FamilyGroupId, 
                opt => opt.MapFrom(src => src.FamilyGroupId)).ReverseMap();
        }
    }
}
