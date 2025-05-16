
using MyVaccine.WebApi.Dtos.UsersAllergy;

namespace MyVaccine.WebApi.Services.Contracts
{
    public interface IUsersAllergyService
    {
        Task<IEnumerable<UsersAllergyResponseDto>> GetAll();
        Task<UsersAllergyResponseDto> GetById(string id);
        Task<UsersAllergyResponseDto> Add(UsersAllergyRequestDto request);
        Task<UsersAllergyResponseDto> Update(UsersAllergyRequestDto request, string id);
        Task<UsersAllergyResponseDto> Delete(string id);

    }
}
