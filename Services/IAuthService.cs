using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;

namespace AspNetCoreRestfulApi.Services;

public interface IAuthService
{
    public Task<TokenResponseDto> Register(RegisterRequestDto userRequestDto);
    
    public Task<TokenResponseDto> Login(LoginRequestDto loginRequestDto);
    
    public string Logout(string accessToken);
    
}