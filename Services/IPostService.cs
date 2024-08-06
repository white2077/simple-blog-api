using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;

namespace AspNetCoreRestfulApi.Services
{
    public interface IPostService : ISevice<PostRequestDto, PostResponseDTO, int>
    {
        public PostResponseDTO CreatePost(PostRequestDto post, int userId);
        
        public PostResponseDTO EditPost(int postId,int userId, PostRequestDto post);
        
        void UserDeletePost(int postId, int userId);
    }
}
