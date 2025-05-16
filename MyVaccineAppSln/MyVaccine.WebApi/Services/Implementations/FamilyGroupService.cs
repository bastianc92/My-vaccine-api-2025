using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

public class FamilyGroupService : IFamilyGroupService
{
    private readonly IBaseRepository<FamilyGroup> _familyGroupRepository;
    private readonly IMapper _mapper;

    private readonly UserManager<User> _userManager;

    public FamilyGroupService(IBaseRepository<FamilyGroup> familyGroupRepository, IMapper mapper, UserManager<User> userManager)
    {
        _familyGroupRepository = familyGroupRepository;
        _mapper = mapper;
        _userManager = userManager;
    }


    public async Task<FamilyGroupResponseDto> Add(FamilyGroupRequestDto request, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var familyGroups = new FamilyGroup();
        familyGroups.FamilyGroupId = Guid.NewGuid().ToString();
        familyGroups.Name = request.Name;
        familyGroups.Users.Add(user);
        await _familyGroupRepository.Add(familyGroups);
        var response = _mapper.Map<FamilyGroupResponseDto>(familyGroups);
        return response;
    }

    public async Task<FamilyGroupResponseDto> Delete(string id)
    {
        var familyGroups = await _familyGroupRepository.FindBy(x => x.FamilyGroupId == id).FirstOrDefaultAsync();

        await _familyGroupRepository.Delete(familyGroups);
        var response = _mapper.Map<FamilyGroupResponseDto>(familyGroups);
        return response;
    }

    public async Task<IEnumerable<FamilyGroupResponseDto>> GetAll()
    {
        var familyGroups = await _familyGroupRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<FamilyGroupResponseDto>>(familyGroups);
        return response;
    }

    public async Task<IEnumerable<FamilyGroupResponseDto>> GetAll(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var familyGroups = await _familyGroupRepository.FindByAsNoTracking(x=>x.Users.Any(u=>u.Id== user.Id)).Include(x=>x.Dependents).ToListAsync();
        var response = _mapper.Map<IEnumerable<FamilyGroupResponseDto>>(familyGroups);
        return response;
    }

    public async Task<FamilyGroupResponseDto> GetById(string id)
    {
        var familyGroups = await _familyGroupRepository.FindByAsNoTracking(x => x.FamilyGroupId == id).FirstOrDefaultAsync();
        var response = _mapper.Map<FamilyGroupResponseDto>(familyGroups);
        return response;
    }

    public async Task<FamilyGroupResponseDto> Update(FamilyGroupRequestDto request, string id)
    {
        var familyGroups = await _familyGroupRepository.FindBy(x => x.FamilyGroupId == id).FirstOrDefaultAsync();
        familyGroups.Name = request.Name;


        await _familyGroupRepository.Update(familyGroups);
        var response = _mapper.Map<FamilyGroupResponseDto>(familyGroups);
        return response;
    }


}
