using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreRestfulApi.Services.Ipml;

public class NotificationService : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);

    }
}