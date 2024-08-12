using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AspNetCoreRestfulApi.Core.CustomException;
using AspNetCoreRestfulApi.Data;
using AspNetCoreRestfulApi.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreRestfulApi.Services.Ipml;

public class TokenService(IConfiguration config,AppDbContext context) : ITokenService
{
    private readonly SymmetricSecurityKey _symmetricSecurityKey = 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty));
    

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
    
    public string RevokeToken(string accessToken)
    {
        ValidateToken(accessToken);
        context.TokenBlackLists.Add(new TokenBlackList()
        {
            Token = accessToken
        });
        context.SaveChanges();
        return accessToken + " revoked";
    }

    public bool IsTokenBlacklisted(string accessToken)
    {
        return context.TokenBlackLists.Any(t => t.Token == accessToken);
    }

    private bool ValidateToken(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = _symmetricSecurityKey
            }, out var validatedToken);
            return true;
        }
        catch (Exception e)
        {
            throw new HttpResponseException((int)HttpStatusCode.Unauthorized,"Token is invalid");
        }
    }
    
}