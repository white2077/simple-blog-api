using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRestfulApi.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "User")]
    [Route("/api/v1/post")]
    public class PostController(IPostService postService) : ControllerBase
    {
        [HttpGet("all")]
        public ActionResult GetAll(int page, int size)
        {
            return Ok(postService.GetAll(page, size));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(postService.GetById(id));
        }

        [HttpPost("create")]
        public ActionResult Create(PostRequestDto post)
        {
            return Ok(postService.Create(post));
        }

        [HttpPut("update/{id}")]
        public ActionResult Update(int id, PostRequestDto post)
        {
            return Ok(postService.Update(id, post));
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            postService.Delete(id);
            return Ok();
        }

    }
}
