using MyVaccine.WebApi.Dtos.VaccineRecord;

namespace MyVaccine.WebApi.Services.Contracts
{
    public interface IVaccineRecordService
    {
        Task<IEnumerable<VaccineRecordResponseDto>> GetAll();
        Task<VaccineRecordResponseDto> GetById(string id);
        Task<VaccineRecordResponseDto> Add(VaccineRecordRequestDto request);
        Task<VaccineRecordResponseDto> Update(VaccineRecordRequestDto request, string id);
        Task<VaccineRecordResponseDto> Delete(string id);
    }
}
