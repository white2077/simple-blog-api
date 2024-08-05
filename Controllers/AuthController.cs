using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRestfulApi.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequestDto user)
    {
        var registeredUser = await authService.Register(user);
        return Ok(registeredUser);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequestDto user)
    {
        var loggedUser = await authService.Login(user);
        return Ok(loggedUser);
    }
    
}