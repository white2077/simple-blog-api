using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Entity;
using AspNetCoreRestfulApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRestfulApi.Controllers
{
    [ApiController]
    [Route("/api/v1")]
    public class PostController : ControllerBase
    {
        IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("all")]
        public ActionResult GetAll(int page, int size)
        {
            return Ok(_postService.GetAll(page, size));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_postService.GetById(id));
        }

        [HttpPost("create")]
        public ActionResult Create(PostRequestDTO post)
        {
            return Ok(_postService.Create(post));
        }

        [HttpPut("update/{id}")]
        public ActionResult Update(int id, PostRequestDTO post)
        {
            return Ok(_postService.Update(id, post));
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            _postService.Delete(id);
            return Ok();
        }

    }
}
