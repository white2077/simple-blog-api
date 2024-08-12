using AspNetCoreRestfulApi.Entities;

namespace AspNetCoreRestfulApi.Services;

public interface ITokenService
{
    public string GenerateAccessToken(User user,IList<string> userRoles);
    
    public string GenerateRefreshToken();
    
    public string RevokeToken(string accessToken);
    
    public bool IsTokenBlacklisted(string accessToken);
    
}