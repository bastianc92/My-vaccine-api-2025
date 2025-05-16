using AutoMapper;
using MyVaccine.WebApi.Dtos.VaccineRecord;
using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Configurations.AutoMapperProfiles
{
    public class VaccineRecordMapping : Profile
    {
        public VaccineRecordMapping()
        {
            CreateMap<VaccineRecord, VaccineRecordRequestDto>().ReverseMap();
            CreateMap<VaccineRecord, VaccineRecordResponseDto>()
                .ForMember(dest => dest.VaccineRecordId, opt => opt.MapFrom(src => src.VaccineRecordId)).ReverseMap();
        }
    }
}
