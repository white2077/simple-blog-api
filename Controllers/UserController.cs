using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Services;
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
        public ActionResult<UserResponseDTO> Update(int id, UserRequestDTO user)
        {
            return Ok(userService.Update(id, user));
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            userService.Delete(id);
            return Ok();
        }
    }
}
