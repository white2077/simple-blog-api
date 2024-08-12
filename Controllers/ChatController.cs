using System.Security.Claims;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Entities;
using AspNetCoreRestfulApi.Services;
using AspNetCoreRestfulApi.Services.Ipml;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreRestfulApi.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController(IChatService chatService):ControllerBase
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(string message)
    {
        var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await chatService.SendGlobalMessage(uid,message);
        return Ok("Message sent");
    }
    [HttpGet("all")]
    public ActionResult<GlobalChatResponseDto> AllChat(int page,int size)
    {
        return Ok(chatService.AllChat(page,size));
    }
    
}