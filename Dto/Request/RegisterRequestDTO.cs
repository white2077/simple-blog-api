namespace AspNetCoreRestfulApi.Dto.Request;

public class RegisterRequestDto(string username, string password, string email)
{
    public String Username { get; set; } = username;

    public String Password { get; set; } = password;

    public String Email { get; set; } = email;
    
}