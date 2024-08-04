using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRestfulApi.Controllers
{
    [ApiController]
    [Route("/api/v1/blog")]
    public class BlogController :ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("/error")]
        public ActionResult Error()
        {
            throw new System.Exception("Error");
        }

        [HttpGet("all")]
        public ActionResult GetAll(int page, int size)
        {
            return Ok(_blogService.GetAll(page, size));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_blogService.GetById(id));
        }

        [HttpGet("user/{userId}")]
        public ActionResult GetBlogsByUserId(int userId, int page, int size)
        {
            return Ok(_blogService.getBlogsByUserId(userId, page, size));
        }

        [HttpPost("create")]
        public ActionResult Create(BlogRequestDTO blog)
        {
            return Ok(_blogService.Create(blog));
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            _blogService.Delete(id);
            return Ok();
        }

    }
}
