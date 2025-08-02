using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDotNet9.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public ActionResult<User> Register(UserDto request)
    {
        var hashedPassword = 
    }

}
