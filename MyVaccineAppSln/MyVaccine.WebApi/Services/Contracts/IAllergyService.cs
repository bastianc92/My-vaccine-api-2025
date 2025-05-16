using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Dtos.FamilyGroup;


namespace MyVaccine.WebApi.Services.Contracts
{
    public interface IAllergyService
    {
        Task<IEnumerable<AllergyResponseDto>> GetAll();
        Task<AllergyResponseDto> GetById(string id);
        Task<AllergyResponseDto> Add(AllergyRequestDto request);
        Task<AllergyResponseDto> Update(AllergyRequestDto request, string id);
        Task<AllergyResponseDto> Delete(string id);
       
    }
}
