using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

public class DependentService : IDependentService
{
    private readonly IBaseRepository<Dependent> _dependentRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public DependentService(IBaseRepository<Dependent> dependentRepository, IMapper mapper, UserManager<User> userManager)
    {
        _dependentRepository = dependentRepository;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<DependentResponseDto> Add(DependentRequestDto request, string userName )
    {
        // var dependents = await _dependentRepository.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();
        var user = await _userManager.FindByNameAsync(userName);
        var dependents = new Dependent();
        dependents.DependentId = Guid.NewGuid().ToString();
        dependents.Name = request.Name;
        dependents.DateOfBirth = request.DateOfBirth;
        dependents.FamilyGroupId = request.FamilyGroupId;   
        dependents.UserId = user.Id;

        await _dependentRepository.Add(dependents);
        var response = _mapper.Map<DependentResponseDto>(dependents);
        return response;
    }

    public async Task<DependentResponseDto> Delete(string id)
    {
        var dependents = await _dependentRepository.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();
    
        await _dependentRepository.Delete(dependents);
        var response = _mapper.Map<DependentResponseDto>(dependents);
        return response;
    }

    public async Task<IEnumerable<DependentResponseDto>> GetAll()
    {
        var dependents = await _dependentRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<DependentResponseDto>>(dependents);
        return response;
    }
    public async Task<IEnumerable<DependentResponseDto>> GetAll( string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        var dependents = await _dependentRepository.FindByAsNoTracking(x=>x.UserId==user.Id ).ToListAsync();
        var response = _mapper.Map<IEnumerable<DependentResponseDto>>(dependents);
        return response;
    }

    public async Task<DependentResponseDto> GetById(string id)
    {
        var dependents = await _dependentRepository.FindByAsNoTracking(x=>x.DependentId==id).FirstOrDefaultAsync();
        var response = _mapper.Map<DependentResponseDto>(dependents);
        return response;
    }

    public async Task<DependentResponseDto> Update(DependentRequestDto request, string id, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        var dependents = await _dependentRepository.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();
        dependents.Name = request.Name;
        dependents.DateOfBirth= request.DateOfBirth;
        dependents.UserId = user.Id;

        await _dependentRepository.Update(dependents);
        var response = _mapper.Map<DependentResponseDto>(dependents);
        return response;
    }

  
}
