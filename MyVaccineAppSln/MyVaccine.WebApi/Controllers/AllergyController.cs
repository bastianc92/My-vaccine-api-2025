using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergyController : ControllerBase
    {
        private readonly IAllergyService _allergyService;
        private readonly IValidator<AllergyRequestDto> _validator;
        private readonly IMapper _mapper;
        public AllergyController(IAllergyService AllergyService, IMapper mapper, IValidator<AllergyRequestDto> validator)
        {
            _allergyService = AllergyService;
            _mapper = mapper;
            _validator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allergies = await _allergyService.GetAll();
            return Ok(allergies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var allergy = await _allergyService.GetById(id);
            if (allergy == null)
            {
                return NotFound();
            }
            return Ok(allergy);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AllergyRequestDto allergyDto)
        {
            var validationResult = await _validator.ValidateAsync(allergyDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var allergies = await _allergyService.Add(allergyDto);
            return Ok(allergies);
        }



        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(string id, AllergyRequestDto requestDto)
        {
            var validationResult = await _validator.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedAllergy = await _allergyService.Update(requestDto, id);

            if (updatedAllergy == null)
            {
                return NotFound();
            }

            var responseDto = _mapper.Map<AllergyRequestDto>(updatedAllergy);
            return Ok(responseDto);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deletedAllergy = await _allergyService.Delete(id);
            if (deletedAllergy == null)
            {
                return NotFound();
            }
            return Ok(deletedAllergy);
        }




    }
}
