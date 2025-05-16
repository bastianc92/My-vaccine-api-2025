using AutoMapper;
using MyVaccine.WebApi.Dtos;
using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Dtos.UsersAllergy;
using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Configurations.AutoMapperProfiles
{
    public class UsersMapping: Profile
    {
        public UsersMapping()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();

        }
    }
}
