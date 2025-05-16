using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.UsersAllergy;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAllergyController : ControllerBase
    {
        private readonly IUsersAllergyService _usersAllergyService;
        private readonly IValidator<UsersAllergyRequestDto> _validator;
        private readonly IMapper _mapper;
        public UsersAllergyController(IUsersAllergyService UsersAllergyService, IMapper mapper, IValidator<UsersAllergyRequestDto> validator)
        {
            _usersAllergyService = UsersAllergyService;
            _mapper = mapper;
            _validator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allergies = await _usersAllergyService.GetAll();
            return Ok(allergies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var allergy = await _usersAllergyService.GetById(id);
            if (allergy == null)
            {
                return NotFound();
            }
            return Ok(allergy);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UsersAllergyRequestDto allergyDto)
        {
            var validationResult = await _validator.ValidateAsync(allergyDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var allergies = await _usersAllergyService.Add(allergyDto);
            return Ok(allergies);
        }



        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(string id, UsersAllergyRequestDto requestDto)
        {
            var validationResult = await _validator.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedUserAllergy = await _usersAllergyService.Update(requestDto, id);

            if (updatedUserAllergy == null)
            {
                return NotFound();
            }

            var responseDto = _mapper.Map<UsersAllergyRequestDto>(updatedUserAllergy);
            return Ok(responseDto);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deletedUserAllergy = await _usersAllergyService.Delete(id);
            if (deletedUserAllergy == null)
            {
                return NotFound();
            }
            return Ok(deletedUserAllergy);
        }


    }
}
