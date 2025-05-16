using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

public class AllergyService : IAllergyService
{
    private readonly IBaseRepository<Allergy> _allergyRepository;
    private readonly IMapper _mapper;
    public AllergyService(IBaseRepository<Allergy> allergyRepository, IMapper mapper)
    {
        _allergyRepository = allergyRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<AllergyResponseDto>> GetAll()
    {
        var allergies = await _allergyRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<AllergyResponseDto>>(allergies);
        return response;
    }
    public async Task<AllergyResponseDto> GetById(string id)
    {
        var allergies = await _allergyRepository.FindByAsNoTracking(x => x.AllergyId == id).FirstOrDefaultAsync();
        var response = _mapper.Map<AllergyResponseDto>(allergies);
        return response;
    }
    public async Task<AllergyResponseDto> Add(AllergyRequestDto request)
    {

        var allergies = new Allergy();
        allergies.Name = request.Name;
        await _allergyRepository.Add(allergies);
        var response = _mapper.Map<AllergyResponseDto>(allergies);
        return response;
    }

    public async Task<AllergyResponseDto> Update(AllergyRequestDto request, string id)
    {
        var allergies = await _allergyRepository.FindBy(x => x.AllergyId == id).FirstOrDefaultAsync();
        allergies.Name = request.Name;

        await _allergyRepository.Update(allergies);
        var response = _mapper.Map<AllergyResponseDto>(allergies);
        return response;
    }


    public async Task<AllergyResponseDto> Delete(string id)
    {
        var allergies = await _allergyRepository.FindBy(x => x.AllergyId == id).FirstOrDefaultAsync();

        await _allergyRepository.Delete(allergies);
        var response = _mapper.Map<AllergyResponseDto>(allergies);
        return response;
    }




}
  

