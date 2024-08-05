using System.ComponentModel.DataAnnotations;

namespace AspNetCoreRestfulApi.Dto.Request;

public class LoginRequestDto(string username, string password)
{
    [Required]
    public string Username { get; set; } = username;

    [Required]
    public string Password { get; set; } = password;
}