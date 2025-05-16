using MyVaccine.WebApi.Dtos.Dependent;

namespace MyVaccine.WebApi.Services.Contracts;

public interface IDependentService
{
    Task<IEnumerable<DependentResponseDto>> GetAll();
    Task<IEnumerable<DependentResponseDto>> GetAll( string userName);
    Task<DependentResponseDto> GetById(string id);
    Task<DependentResponseDto> Add(DependentRequestDto request, string userName);
    Task<DependentResponseDto> Update(DependentRequestDto request, string id, string userName);
    Task<DependentResponseDto> Delete(string id);
}
