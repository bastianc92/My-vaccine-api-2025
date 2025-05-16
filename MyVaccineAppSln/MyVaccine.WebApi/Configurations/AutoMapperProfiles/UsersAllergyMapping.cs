using AutoMapper;
using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Dtos.UsersAllergy;
using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Configurations.AutoMapperProfiles
{
    public class UsersAllergyMapping: Profile
    {
        public UsersAllergyMapping()
        {
            CreateMap<UsersAllergy, UsersAllergyRequestDto>().ReverseMap();
            CreateMap<UsersAllergy, UsersAllergyResponseDto>()
                .ForMember(dest => dest.UserAllergyId, opt => opt.MapFrom(src => src.UserAllergyId)).ReverseMap();
        }
    }
}
