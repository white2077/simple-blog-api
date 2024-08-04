using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;

namespace AspNetCoreRestfulApi.Services
{
    public interface IBlogService :ISevice<BlogRequestDTO, BlogResponseDTO, int>
    {
        Pageable<BlogResponseDTO> getBlogsByUserId(int userId,int page,int size);
    }
}
