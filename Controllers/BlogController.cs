using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Services;
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
        public ActionResult GetAll(int page, int size)
        {
            return Ok(blogService.GetAll(page, size));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(blogService.GetById(id));
        }

        [HttpGet("user/{userId}")]
        public ActionResult GetBlogsByUserId(int userId, int page, int size)
        {
            return Ok(blogService.getBlogsByUserId(userId, page, size));
        }

        [HttpPost("create")]
        public ActionResult Create(BlogRequestDTO blog)
        {
            return Ok(blogService.Create(blog));
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            blogService.Delete(id);
            return Ok();
        }

    }
}
