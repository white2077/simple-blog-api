using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;

namespace AspNetCoreRestfulApi.Services
{
    public interface IBlogService :ISevice<BlogRequestDto, BlogResponseDto, int>
    {
        Pageable<BlogResponseDto> GetBlogsByUserId(int userId,int page,int size);
    }
}
