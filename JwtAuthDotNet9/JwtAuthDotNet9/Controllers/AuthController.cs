using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;
using JwtAuthDotNet9.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDotNet9.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request, CancellationToken ct)
    {
        var user = await authService.RegisterAsync(request, ct);

        if (user is null)
            return BadRequest("User already exists.");

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> Login(UserDto request, CancellationToken ct)
    {
        var result = await authService.LoginAsync(request, ct);

        if (result is null)
            return BadRequest("Invalid username or password.");

        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request, CancellationToken ct)
    {
        var result = await authService.RefreshTokensAsync(request, ct);

        if (result is null || result.AccessToken is null || result.RefreshToken is null)
            return Unauthorized("Invalid refresh token.");

        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public IActionResult AuthenticatedOnlyEndpoint()
    {
        return Ok("You are authenticated!");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnlyEndpoint()
    {
        return Ok("You are and admin!");
    }
}
