using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.UsersAllergy;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

public class UsersAllergyService : IUsersAllergyService
{
    private readonly IBaseRepository<UsersAllergy> _usersAllergyRepository;
    private readonly IMapper _mapper;
    public UsersAllergyService(IBaseRepository<UsersAllergy> allergyRepository, IMapper mapper)
    {
        _usersAllergyRepository = allergyRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<UsersAllergyResponseDto>> GetAll()
    {
        var usersAllergies = await _usersAllergyRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<UsersAllergyResponseDto>>(usersAllergies);
        return response;
    }
    public async Task<UsersAllergyResponseDto> GetById(string id)
    {
        var usersAllergies = await _usersAllergyRepository.FindByAsNoTracking(x => x.AllergyId == id).FirstOrDefaultAsync();
        var response = _mapper.Map<UsersAllergyResponseDto>(usersAllergies);
        return response;
    }
    public async Task<UsersAllergyResponseDto> Add(UsersAllergyRequestDto request)
    {

        var usersAllergies = new UsersAllergy();
        usersAllergies.UserId = request.UserId;
        usersAllergies.AllergyId = request.AllergyId;
        await _usersAllergyRepository.Add(usersAllergies);
        var response = _mapper.Map<UsersAllergyResponseDto>(usersAllergies);
        return response;
    }

    public async Task<UsersAllergyResponseDto> Update(UsersAllergyRequestDto request, string id)
    {
        var usersAllergies = await _usersAllergyRepository.FindBy(x => x.UserAllergyId == id).FirstOrDefaultAsync();
        usersAllergies.UserId = request.UserId;
        usersAllergies.AllergyId = request.AllergyId;

        await _usersAllergyRepository.Update(usersAllergies);
        var response = _mapper.Map<UsersAllergyResponseDto>(usersAllergies);
        return response;
    }


    public async Task<UsersAllergyResponseDto> Delete(string id)
    {
        var usersAllergies = await _usersAllergyRepository.FindBy(x => x.UserAllergyId == id).FirstOrDefaultAsync();

        await _usersAllergyRepository.Delete(usersAllergies);
        var response = _mapper.Map<UsersAllergyResponseDto>(usersAllergies);
        return response;
    }


}
