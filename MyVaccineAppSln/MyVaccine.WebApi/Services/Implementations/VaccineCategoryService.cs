using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.VaccineCategory;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

public class VaccineCategoryService : IVaccineCategoryService
{
    private readonly IBaseRepository<VaccineCategory> _vaccineCategoryRepository;
    private readonly IMapper _mapper;
    public VaccineCategoryService(IBaseRepository<VaccineCategory> VaccineCategoryRepository, IMapper mapper)
    {
        _vaccineCategoryRepository = VaccineCategoryRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<VaccineCategoryResponseDto>> GetAll()
    {
        var categories = await _vaccineCategoryRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<VaccineCategoryResponseDto>>(categories);
        return response;
    }
    public async Task<VaccineCategoryResponseDto> GetById(string id)
    {
        var categories = await _vaccineCategoryRepository.FindByAsNoTracking(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();
        var response = _mapper.Map<VaccineCategoryResponseDto>(categories);
        return response;
    }
    public async Task<VaccineCategoryResponseDto> Add(VaccineCategoryRequestDto request)
    {

        var categories = new VaccineCategory();
        categories.Name = request.Name;
        await _vaccineCategoryRepository.Add(categories);
        var response = _mapper.Map<VaccineCategoryResponseDto>(categories);
        return response;
    }

    public async Task<VaccineCategoryResponseDto> Update(VaccineCategoryRequestDto request, string id)
    {
        var categories = await _vaccineCategoryRepository.FindBy(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();
        categories.Name = request.Name;

        await _vaccineCategoryRepository.Update(categories);
        var response = _mapper.Map<VaccineCategoryResponseDto>(categories);
        return response;
    }


    public async Task<VaccineCategoryResponseDto> Delete(string id)
    {
        var categories = await _vaccineCategoryRepository.FindBy(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();

        await _vaccineCategoryRepository.Delete(categories);
        var response = _mapper.Map<VaccineCategoryResponseDto>(categories);
        return response;
    }
}
