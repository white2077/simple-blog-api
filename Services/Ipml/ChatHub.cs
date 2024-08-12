using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreRestfulApi.Services.Ipml;

[Authorize]
public class ChatHub(ILogger<ChatHub> logger) : Hub
{
    public async Task SendMessage(string message)
    {
        var userName = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        await Clients.All.SendAsync("ReceiveMessage", userName, message);
        logger.LogInformation($"User {userName} sent message: {message}");
    }
    
    
    public override async Task OnConnectedAsync()
    {
        LogInformation(Context, "connected");
        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        LogInformation(Context, "disconnected");
        return base.OnDisconnectedAsync(exception);
    }

    protected override void Dispose(bool disposing)
    {
        LogInformation(Context, "disposed");
        base.Dispose(disposing);
    }

    private void LogInformation(HubCallerContext context,string message)
    {
        var userId = context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userName = context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var clientId = context.ConnectionId;
        logger.Log(LogLevel.Information, $"Client: {clientId}, userId: {userId}, userName: {userName} {message}");
    }
}