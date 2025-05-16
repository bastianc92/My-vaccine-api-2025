using MyVaccine.WebApi.Dtos.Vaccine;

namespace MyVaccine.WebApi.Services.Contracts
{
    public interface IVaccineService
    {
        Task<IEnumerable<VaccineResponseDto>> GetAll();
        Task<VaccineResponseDto> GetById(string id);
        Task<VaccineResponseDto> Add(VaccineRequestDto request);
        Task<VaccineResponseDto> Update(VaccineRequestDto request, string id);
        Task<VaccineResponseDto> Delete(string id);
    }
}
