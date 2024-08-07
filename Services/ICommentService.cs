using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;

namespace AspNetCoreRestfulApi.Services;

public interface ICommentService
{
    public Pageable<CommentResponseDto> GetAllCommentByPostId(int postId, int page, int size);
    
    public CommentResponseDto CreateComment(CommentRequestDto comment,int userId);
}