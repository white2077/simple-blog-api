using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCoreRestfulApi.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreRestfulApi.Services.Ipml;

public class TokenService(IConfiguration config) : ITokenService
{
    private readonly SymmetricSecurityKey _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty));
    

    public string GenerateAccessToken(User user,IList<string> userRoles)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email??string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(ClaimTypes.AuthenticationMethod, "JWT"),
        };
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var credentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDiscriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(1),
            SigningCredentials = credentials,
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"]
        };
        
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDiscriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        return JwtRegisteredClaimNames.Jti + Guid.NewGuid().ToString();
    }
}