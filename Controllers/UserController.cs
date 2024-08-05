using System.Security.Claims;
using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRestfulApi.Controllers
{
    [ApiController]
    [Route("/api/v1/user")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("all")]
        public ActionResult<Pageable<UserResponseDTO>> GetAll(int page, int size)
        {
            return Ok(userService.GetAll(page, size));
        }
        [HttpGet("{id}")]
        public ActionResult<UserResponseDTO> GetById(int id)
        {
            return Ok(userService.GetById(id));
        }
        
        [HttpPut("update/{id}")]
        public ActionResult<UserResponseDTO> Update(int id, UserRequestDto user)
        {
            return Ok(userService.Update(id, user));
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            userService.Delete(id);
            return Ok();
        }
        [HttpGet("info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "User")]
        public ActionResult GetUserInfor()
        {
            
            return Ok(new UserResponseDTO()
            {
                Id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Name = User.FindFirstValue(ClaimTypes.Name),
                Email = User.FindFirstValue(ClaimTypes.Email),
            });
        }
        
    }
}
