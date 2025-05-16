using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.Vaccine;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

    public class VaccineService : IVaccineService
    {

        private readonly IBaseRepository<Vaccine> _vaccineRepository;
        private readonly IMapper _mapper;
        public  VaccineService(IBaseRepository<Vaccine> vaccineRepository, IMapper mapper)
        {
            _vaccineRepository = vaccineRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<VaccineResponseDto>> GetAll()
        {
            var vaccines = await _vaccineRepository.GetAll().AsNoTracking().ToListAsync();
            var response = _mapper.Map<IEnumerable<VaccineResponseDto>>(vaccines);
            return response;
        }
        public async Task<VaccineResponseDto> GetById(string id)
        {
            var vaccines = await _vaccineRepository.FindByAsNoTracking(x => x.VaccineId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<VaccineResponseDto>(vaccines);
            return response;
        }
        public async Task<VaccineResponseDto> Add(VaccineRequestDto request)
        {

            var vaccines = new Vaccine();
            vaccines.Name = request.Name;
            vaccines.RequiresBooster = request.RequiresBooster;
            await _vaccineRepository.Add(vaccines);
            var response = _mapper.Map<VaccineResponseDto>(vaccines);
            return response;
        }

        public async Task<VaccineResponseDto> Update(VaccineRequestDto request, string id)
        {
            var vaccines = await _vaccineRepository.FindBy(x => x.VaccineId == id).FirstOrDefaultAsync();
            vaccines.Name = request.Name;
            vaccines.RequiresBooster = request.RequiresBooster;

            await _vaccineRepository.Update(vaccines);
            var response = _mapper.Map<VaccineResponseDto>(vaccines);
            return response;
        }


        public async Task<VaccineResponseDto> Delete(string id)
        {
            var vaccines = await _vaccineRepository.FindBy(x => x.VaccineId == id).FirstOrDefaultAsync();

            await _vaccineRepository.Delete(vaccines);
            var response = _mapper.Map<VaccineResponseDto>(vaccines);
            return response;
        }



    }

