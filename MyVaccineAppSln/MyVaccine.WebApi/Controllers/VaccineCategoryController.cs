using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.VaccineCategory;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineCategoryController : ControllerBase
    {
        private readonly IVaccineCategoryService _vaccinecategoryservice;
        private readonly IValidator<VaccineCategoryRequestDto> _validator;
        private readonly IMapper _mapper;

        public VaccineCategoryController(IVaccineCategoryService Vaccinecategoryservice, IMapper mapper, IValidator<VaccineCategoryRequestDto> validator)
        {
            _vaccinecategoryservice = Vaccinecategoryservice;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vaccineC = await _vaccinecategoryservice.GetAll();
            return Ok(vaccineC);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var vaccineC = await _vaccinecategoryservice.GetById(id);
            return Ok(vaccineC);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VaccineCategoryRequestDto categoriesDto)
        {
            var validationResult = await _validator.ValidateAsync(categoriesDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var vaccineC = await _vaccinecategoryservice.Add( categoriesDto);
            return Ok(vaccineC);
        }


        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(string id, VaccineCategoryRequestDto requestDto)
        {
            var validationResult = await _validator.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedVaccineC = await _vaccinecategoryservice.Update(requestDto, id);

            if (updatedVaccineC == null)
            {
                return NotFound();
            }

            var responseDto = _mapper.Map<VaccineCategoryResponseDto>(updatedVaccineC);
            return Ok(responseDto);
        }

        [HttpPost("delete/{id}")]

        public async Task<IActionResult> Delete(string id)
        {
            var deletedVaccineC = await _vaccinecategoryservice.Delete(id);

            if (deletedVaccineC == null)
            {
                return NotFound();
            }

            var responseDto = _mapper.Map<VaccineCategoryResponseDto>(deletedVaccineC);
            return Ok(responseDto);
        }
    }
}
