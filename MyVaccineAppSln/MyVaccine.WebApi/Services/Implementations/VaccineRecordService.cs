using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.VaccineRecord;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

    public class VaccineRecordService : IVaccineRecordService
    {
        private readonly IBaseRepository<VaccineRecord> _vaccineRecordRepository;
        private readonly IMapper _mapper;
        public VaccineRecordService(IBaseRepository<VaccineRecord> VaccineRecordRepository, IMapper mapper)
        {
            _vaccineRecordRepository = VaccineRecordRepository;
            _mapper = mapper;
        }
        public async Task<VaccineRecordResponseDto> Add(VaccineRecordRequestDto request)
        {
       
            var vaccineRecords = new VaccineRecord(); 
            vaccineRecords.UserId = request.UserId;
            vaccineRecords.DependentId = request.DependentId;
            vaccineRecords.VaccineId = request.VaccineId;
            vaccineRecords.DateAdministered = request.DateAdministered;
            vaccineRecords.AdministeredLocation = request.AdministeredLocation;
            vaccineRecords.AdministeredBy = request.AdministeredBy;


            await _vaccineRecordRepository.Add(vaccineRecords);
            var response = _mapper.Map<VaccineRecordResponseDto>(vaccineRecords);
            return response;
        }

        public async Task<VaccineRecordResponseDto> Delete(string id)
        {
            var vaccineRecords = await _vaccineRecordRepository.FindBy(x => x.VaccineRecordId == id).FirstOrDefaultAsync();

            await _vaccineRecordRepository.Delete(vaccineRecords);
            var response = _mapper.Map<VaccineRecordResponseDto>(vaccineRecords);
            return response;
        }

        public async Task<IEnumerable<VaccineRecordResponseDto>> GetAll()
        {
            var vaccineRecords = await _vaccineRecordRepository.GetAll().AsNoTracking().ToListAsync();
            var response = _mapper.Map<IEnumerable<VaccineRecordResponseDto>>(vaccineRecords);
            return response;
        }

        public async Task<VaccineRecordResponseDto> GetById(string id)
        {
            var vaccineRecords = await _vaccineRecordRepository.FindByAsNoTracking(x => x.VaccineRecordId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<VaccineRecordResponseDto>(vaccineRecords);
            return response;
        }

        public async Task<VaccineRecordResponseDto> Update(VaccineRecordRequestDto request, string id)
        {
            var vaccineRecords = await _vaccineRecordRepository.FindBy(x => x.VaccineRecordId == id).FirstOrDefaultAsync();
            vaccineRecords.UserId = request.UserId;
            vaccineRecords.DependentId = request.DependentId;
            vaccineRecords.VaccineId = request.VaccineId;
            vaccineRecords.DateAdministered = request.DateAdministered;
            vaccineRecords.AdministeredLocation = request.AdministeredLocation;
            vaccineRecords.AdministeredBy = request.AdministeredBy;

            await _vaccineRecordRepository.Update(vaccineRecords);
            var response = _mapper.Map<VaccineRecordResponseDto>(vaccineRecords);
            return response;
        }

    }

