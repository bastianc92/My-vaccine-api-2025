using System.Security.AccessControl;
using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DependentsController : ControllerBase
{
    private readonly IDependentService _dependentService;
    private readonly IValidator<DependentRequestDto> _validator;
    private readonly IMapper _mapper;

    public DependentsController(IDependentService dependentService,IMapper mapper, IValidator<DependentRequestDto> validator)
    {
        _dependentService = dependentService;
        _mapper = mapper ;
        _validator = validator;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

        var dependents = await _dependentService.GetAll(claimsIdentity.Name);
        return Ok(dependents);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var dependents = await _dependentService.GetById(id);
        return Ok(dependents);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(DependentRequestDto dependentsDto)
    {
        var validationResult = await _validator.ValidateAsync(dependentsDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        var dependents = await _dependentService.Add(dependentsDto, claimsIdentity.Name);
        return Ok(dependents);
    }


    [Authorize]
    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(string id, DependentRequestDto requestDto)
    {
        var validationResult = await _validator.ValidateAsync(requestDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

        var updatedDependent = await _dependentService.Update(requestDto, id, claimsIdentity.Name);

        if (updatedDependent == null)
        {
            return NotFound();
        }

        var responseDto = _mapper.Map<DependentResponseDto>(updatedDependent);
        return Ok(responseDto);
    }


    [Authorize]
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deletedDependent = await _dependentService.Delete(id);

        if (deletedDependent == null)
        {
            return NotFound(); 
        }

        var responseDto = _mapper.Map<DependentResponseDto>(deletedDependent);
        return Ok(responseDto); 
    }
}
