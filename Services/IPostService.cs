using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;

namespace AspNetCoreRestfulApi.Services
{
    public interface IPostService : ISevice<PostRequestDto, PostResponseDTO, int>
    {

    }
}
