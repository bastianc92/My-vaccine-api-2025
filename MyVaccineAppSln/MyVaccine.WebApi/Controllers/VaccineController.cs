using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.Vaccine;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _vaccineservice;
        private readonly IValidator<VaccineRequestDto> _validator;
        private readonly IMapper _mapper;

        public VaccineController(IVaccineService vaccineservice, IMapper mapper, IValidator<VaccineRequestDto> validator)
        {
            _vaccineservice = vaccineservice;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vaccines = await _vaccineservice.GetAll();
            return Ok(vaccines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var vaccines = await _vaccineservice.GetById(id);
            return Ok(vaccines);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VaccineRequestDto vaccinesDto)
        {
            var validationResult = await _validator.ValidateAsync(vaccinesDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var vaccines = await _vaccineservice.Add(vaccinesDto);
            return Ok(vaccines);
        }


        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(string id, VaccineRequestDto requestDto)
        {
            var validationResult = await _validator.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedVaccine = await _vaccineservice.Update(requestDto, id);

            if (updatedVaccine == null)
            {
                return NotFound();
            }

            var responseDto = _mapper.Map<VaccineResponseDto>(updatedVaccine);
            return Ok(responseDto);
        }

        [HttpPost("delete/{id}")]

        public async Task<IActionResult> Delete(string id)
        {
            var deletedVaccine = await _vaccineservice.Delete(id);

            if (deletedVaccine == null)
            {
                return NotFound();
            }

            var responseDto = _mapper.Map<VaccineResponseDto>(deletedVaccine);
            return Ok(responseDto);
        }
    }
}
