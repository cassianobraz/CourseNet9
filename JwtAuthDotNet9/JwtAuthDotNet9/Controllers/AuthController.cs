using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;
using JwtAuthDotNet9.Services;
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
    public async Task<ActionResult<string>> Login(UserDto request, CancellationToken ct)
    {
        var token = await authService.LoginAsync(request, ct);

        if (token is null)
            return BadRequest("Invalid username or password.");

        return Ok(token);
    }

    [HttpGet]
    public IActionResult AuthenticatedOnlyEndpoint()
    {
        return Ok("You are authenticated!");
    }
}
