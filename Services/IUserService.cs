using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;

namespace AspNetCoreRestfulApi.Services
{
    public interface IUserService : ISevice<UserRequestDto,UserResponseDTO, int>
    {
    }
}
