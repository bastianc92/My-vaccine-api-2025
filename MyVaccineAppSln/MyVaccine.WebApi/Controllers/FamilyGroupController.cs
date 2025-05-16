using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Services.Contracts;
using System.Security.Claims;

namespace MyVaccine.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyGroupController : ControllerBase
    {
        private readonly IFamilyGroupService _familyGroupService;
        private readonly IValidator<FamilyGroupRequestDto> _validator;
        private readonly IMapper _mapper;
        public FamilyGroupController(IFamilyGroupService familyGroupService, IMapper mapper, IValidator<FamilyGroupRequestDto> validator)
        {
            _familyGroupService = familyGroupService;
            _mapper = mapper;
            _validator = validator;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var familyGroups = await _familyGroupService.GetAll();
            return Ok(familyGroups);
        }

        [Authorize]
        [HttpGet("mine")]
        public async Task<IActionResult> GetAllByUser()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var familyGroups = await _familyGroupService.GetAll(claimsIdentity.Name);
            return Ok(familyGroups);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var familyGroup = await _familyGroupService.GetById(id);
            if (familyGroup == null)
            {
                return NotFound();
            }
            return Ok(familyGroup);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(FamilyGroupRequestDto familyGroupDto)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

            var validationResult = await _validator.ValidateAsync(familyGroupDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var familyGroups = await _familyGroupService.Add(familyGroupDto, claimsIdentity.Name);
            return Ok(familyGroups);
        }


        [Authorize]
        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(string id, FamilyGroupResponseDto familyGroupDto)
        {
            var updatedFamilyGroup = await _familyGroupService.Update(familyGroupDto, id);
            if (updatedFamilyGroup == null)
            {
                return NotFound();
            }
            return Ok(updatedFamilyGroup);
        }

        [Authorize]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deletedFamilyGroup = await _familyGroupService.Delete(id);
            if (deletedFamilyGroup == null)
            {
                return NotFound();
            }
            return Ok(deletedFamilyGroup);
        }




    }
}
