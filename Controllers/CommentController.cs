using System.Security.Claims;
using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRestfulApi.Controllers;

[ApiController]
[Route("api/v1/comments")]
public class CommentController(ICommentService commentService) : ControllerBase
{
    [HttpGet("{postId:int}")]
    public ActionResult<Pageable<CommentResponseDto>> GetAllCommentByPostId(int postId, int page , int size)
    {
        return Ok(commentService.GetAllCommentByPostId(postId, page, size));
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,User") ]

    public ActionResult<CommentResponseDto> CreateComment(CommentRequestDto  comment)
    {
        return Ok(commentService.CreateComment(comment, int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
    }
}