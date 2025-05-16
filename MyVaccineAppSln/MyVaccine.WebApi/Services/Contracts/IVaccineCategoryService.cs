using MyVaccine.WebApi.Dtos.VaccineCategory;

namespace MyVaccine.WebApi.Services.Contracts
{
    public interface IVaccineCategoryService
    {
        Task<IEnumerable<VaccineCategoryResponseDto>> GetAll();
        Task<VaccineCategoryResponseDto> GetById(string id);
        Task<VaccineCategoryResponseDto> Add(VaccineCategoryRequestDto request);
        Task<VaccineCategoryResponseDto> Update(VaccineCategoryRequestDto request, string id);
        Task<VaccineCategoryResponseDto> Delete(string id);
    }
}
