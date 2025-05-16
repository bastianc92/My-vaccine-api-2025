
using MyVaccine.WebApi.Dtos.FamilyGroup;

namespace MyVaccine.WebApi.Services.Contracts
{
    public interface IFamilyGroupService
    {
        Task<IEnumerable<FamilyGroupResponseDto>> GetAll();
        Task<IEnumerable<FamilyGroupResponseDto>> GetAll(string userName);
        Task<FamilyGroupResponseDto> GetById(string id);
        Task<FamilyGroupResponseDto> Add(FamilyGroupRequestDto request, string userName);
        Task<FamilyGroupResponseDto> Update(FamilyGroupRequestDto request, string id);
        Task<FamilyGroupResponseDto> Delete(string id);
    }
}
