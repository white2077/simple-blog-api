using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Entities;
using AspNetCoreRestfulApi.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRestfulApi.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController(UserManager<User> userManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RegisterRequestDto user)
    {
        var userEntity = new User()
        {
            Name = user.Username,
            UserName = user.Username,
            Email = user.Email
        };

        var result = await userManager.CreateAsync(userEntity, user.Password);

        if (result.Succeeded)
        {
            var roleResult = await userManager.AddToRoleAsync(userEntity, "user");
            return Ok(user);
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }
}