using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Data;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Entities;
using AspNetCoreRestfulApi.Services.Ipml;
using AspNetCoreRestfulApi.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRestfulApi.Services;

public class ChatService(ILogger<ChatService> logger,IHubContext<ChatHub> hubContext,AppDbContext dbContext):IChatService
{
    public Task SendGlobalMessage(int userId, string message)
    {
        var chat = new GlobalChat()
        {
            UserId = userId,
            Message = message,
            CreatedAt = DateTime.UtcNow
        };
        var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
        dbContext.GlobalChats.Add(chat);
        dbContext.SaveChanges();
        return hubContext.Clients.All.SendAsync("ReceiveMessage",user.Name,message);
    }

    public Pageable<GlobalChatResponseDto> AllChat(int page, int size)
    {
       var allChat = dbContext.GlobalChats.Select(c => new GlobalChatResponseDto()
        {
            Username = c.User.Name,
            Message = c.Message,
            CreatedAt = c.CreatedAt
        })
        .OrderByDescending(c => c.CreatedAt)
        .ToPageable(page, size);
       allChat.Items.Reverse();
       return allChat;
    }
}