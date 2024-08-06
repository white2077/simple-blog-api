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
    [Route("/api/v1/post")]
    public class PostController(IPostService postService) : ControllerBase
    {
        [HttpGet("all")]
        public ActionResult<Pageable<PostResponseDTO>> GetAll(int page, int size)
        {
            return Ok(postService.GetAll(page, size));
        }

        [HttpGet("{id:int}")]
        public ActionResult GetById(int id)
        {
            return Ok(postService.GetById(id));
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,User")]
        public ActionResult<PostResponseDTO> Create(PostRequestDto post)
        {
            return Ok(postService.CreatePost(post,int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }

        [HttpPut("edit/{id:int}")]
        public ActionResult Update(int id, PostRequestDto post)
        {
            return Ok(postService.EditPost(id,int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), post));
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            postService.Delete(id);
            return Ok("Delete Post Success");
        }
        [HttpDelete("user/delete/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,User")]
        public ActionResult UserDelete(int id)
        {
            postService.UserDeletePost(id,int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return Ok("Delete Post Success");
        }

    }
}
