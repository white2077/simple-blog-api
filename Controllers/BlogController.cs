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
    [Route("/api/v1/blog")]
    public class BlogController(IBlogService blogService) : ControllerBase
    {
        [HttpGet("/error")]
        public ActionResult Error()
        {
            throw new System.Exception("Error");
        }

        [HttpGet("all")]
        public ActionResult<Pageable<BlogResponseDto>> GetAll(int page, int size)
        {
            return Ok(blogService.GetAll(page, size));
        }
        
        [HttpGet("{id:int}")]
        public ActionResult<BlogResponseDto> GetById(int id)
        {
            return Ok(blogService.GetById(id));
        }

        [HttpGet("user/{userId:int}")]
        public ActionResult<Pageable<BlogResponseDto>>  GetBlogsByUserId(int userId, int page, int size)
        {
            return Ok(blogService.GetBlogsByUserId(userId, page, size));
        }
        
        
        [HttpPost("createBlog")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public ActionResult<BlogResponseDto> CreateBlog(BlogRequestDto blog)
        {
            return Ok(blogService.CreateBlog(blog, int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
            
        }
        [HttpPut("update/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public ActionResult<BlogResponseDto> Update(int id,BlogRequestDto blog)
        {
            return Ok(blogService.Update(id,blog));
        }
        
        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public ActionResult<String> Delete(int id)
        {
            blogService.Delete(id);
            return Ok("Delete success");
        }
        
        

    }
}
