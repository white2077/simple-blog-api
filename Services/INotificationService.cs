namespace AspNetCoreRestfulApi.Services;

public interface INotificationService
{
    public  Task SendAsync(string email, string subject, string message);   
}