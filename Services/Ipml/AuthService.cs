using System.Net;
using AspNetCoreRestfulApi.Core.CustomException;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRestfulApi.Services.Ipml;

public class AuthService(UserManager<User> userManager,ITokenService tokenService,SignInManager<User> signInManager) : IAuthService
{
    public async Task<TokenResponseDto> Register(RegisterRequestDto userRequestDto)
    {
        var userEntity = new User()
        {
            Name = userRequestDto.Username,
            UserName = userRequestDto.Username,
            Email = userRequestDto.Email
        };

        var result = await userManager.CreateAsync(userEntity, userRequestDto.Password);

        if (result.Succeeded)
        {
            var roleResult = await userManager.AddToRoleAsync(userEntity, "User");
            return new TokenResponseDto()
            {
                AccessToken = tokenService.GenerateAccessToken(userEntity, ["User"]),
                RefreshToken = tokenService.GenerateRefreshToken()
            };
        }
        else
        {
            throw new HttpResponseException((int)HttpStatusCode.BadRequest, result.Errors);
        }
    }

    public async Task<TokenResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginRequestDto.Username)
                   ?? throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "Bad credentials");
        var result = await signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);


        var role = await signInManager.UserManager.GetRolesAsync(user);
        var tokenResponseDto = result.Succeeded ?  new TokenResponseDto()
        {
            
            AccessToken = tokenService.GenerateAccessToken(user,role),
            RefreshToken = tokenService.GenerateRefreshToken()
        } : throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "Bad credentials");
        
        return tokenResponseDto;
    }

    public string Logout(string accessToken)
    {
        return tokenService.RevokeToken(accessToken);
    }
}