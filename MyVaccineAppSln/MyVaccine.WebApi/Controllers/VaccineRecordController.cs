using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.VaccineRecord;
using MyVaccine.WebApi.Services.Contracts;
using MyVaccine.WebApi.Services.Implementations;

namespace MyVaccine.WebApi.Controllers { 

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class VaccineRecordController : ControllerBase
{
    private readonly IVaccineRecordService _vaccineRecordService;
    private readonly IValidator<VaccineRecordRequestDto> _validator;
    private readonly IMapper _mapper;

    public VaccineRecordController(IVaccineRecordService vaccineRecordService, IMapper mapper, IValidator<VaccineRecordRequestDto> validator)
    {
        _vaccineRecordService = vaccineRecordService;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vaccinesR = await _vaccineRecordService.GetAll();
        return Ok(vaccinesR);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var vaccinesR = await _vaccineRecordService.GetById(id);
        return Ok(vaccinesR);
    }

    [HttpPost]
    public async Task<IActionResult> Create(VaccineRecordRequestDto vaccinesRDto)
    {
        var validationResult = await _validator.ValidateAsync(vaccinesRDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var vaccinesR = await _vaccineRecordService.Add(vaccinesRDto);
        return Ok(vaccinesR);
    }


    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(string id, VaccineRecordRequestDto requestDto)
    {
        var validationResult = await _validator.ValidateAsync(requestDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var updatedVaccineRecord = await _vaccineRecordService.Update(requestDto, id);

        if (updatedVaccineRecord == null)
        {
            return NotFound();
        }

        var responseDto = _mapper.Map<VaccineRecordResponseDto>(updatedVaccineRecord);
        return Ok(responseDto);
    }

    [HttpPost("delete/{id}")]

    public async Task<IActionResult> Delete(string id)
    {
        var deletedVaccineRecord = await _vaccineRecordService.Delete(id);

        if (deletedVaccineRecord == null)
        {
            return NotFound();
        }

        var responseDto = _mapper.Map<VaccineRecordResponseDto>(deletedVaccineRecord);
        return Ok(responseDto);
    }
}
}
