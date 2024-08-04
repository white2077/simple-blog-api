using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Services;
using AspNetCoreRestfulApi.Services.Ipml;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRestfulApi.Controllers
{
    [ApiController]
    [Route("/api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public ActionResult<Pageable<UserResponseDTO>> GetAll(int page, int size)
        {
            return Ok(_userService.GetAll(page, size));
        }
        [HttpGet("{id}")]
        public ActionResult<UserResponseDTO> GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpPost("create")]
        public ActionResult<UserResponseDTO> Create(UserRequestDTO user)
        {
            return Ok(_userService.Create(user));
        }

        [HttpPut("update/{id}")]
        public ActionResult<UserResponseDTO> Update(int id, UserRequestDTO user)
        {
            return Ok(_userService.Update(id, user));
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
