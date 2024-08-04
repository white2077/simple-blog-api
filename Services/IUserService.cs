using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;

namespace AspNetCoreRestfulApi.Services
{
    public interface IUserService : ISevice<UserRequestDTO,UserResponseDTO, int>
    {
    }
}
