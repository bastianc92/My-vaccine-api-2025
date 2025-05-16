using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyVaccine.WebApi.Dtos;
using MyVaccine.WebApi.Literals;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Repositories.Implementations;
using MyVaccine.WebApi.Services.Contracts;
using MyVaccine.WebApi.Services.Implementations;

namespace MyVaccine.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly SaveProfileImageUserService _saveProfileImageUserService;

    public AuthController( IUserService userService, SaveProfileImageUserService saveProfileImageUserService)
    {
        _userService = userService;
        _saveProfileImageUserService = saveProfileImageUserService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequetDto model)
    {
        var response = await _userService.AddUserAsync(model);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var response = await _userService.Login(model);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return Unauthorized(response);
        }


    }

    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        var response = await _userService.RefreshToken(claimsIdentity.Name);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return Unauthorized(response);
        }
    }

    [Authorize]
    [HttpGet("user-info")]
    public async Task<IActionResult> GetUserInfo()
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        var response = await _userService.GetUserInfo(claimsIdentity.Name);

        return Ok(response);
    }

    [Authorize]
    [HttpPost("update-user-photo-profile")]
    public async Task<IActionResult> UpdateUserPhotoImage(IFormFile file)
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        var response = await _saveProfileImageUserService.SaveImageAsync(file, claimsIdentity.Name);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("get-user-photo-profile")]
    public async Task<IActionResult> GetUserPhotoImage()
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        var (success, bytes)  = await _saveProfileImageUserService.GetImageAsync( claimsIdentity.Name);
        if (!success)
        {
            return NotFound();
        }

        return File(bytes, "image/jpeg");

    }
}


