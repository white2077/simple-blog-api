using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Data;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Entities;
using AspNetCoreRestfulApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRestfulApi.Services.Ipml;

public class CommentService(AppDbContext context) : ICommentService
{
    public Pageable<CommentResponseDto> GetAllCommentByPostId(int postId, int page, int size)
    {
        var comments = context.Comments
            .Include(c => c.ChildrenComments)
            .Where(c => c.post_id == postId && c.comment_id == null)
            .Select(c => new CommentResponseDto()
            {
                Id = c.Id,
                User = new UserResponseDTO()
                {
                    Id = c.User.Id,
                    Name = c.User.Name,
                    Email = c.User.Email
                },
                Content = c.Content,
                ParentCommentId = c.comment_id,
                CreatedAt = c.CreatedAt
            })
            .ToPageable(page, size);
        comments.Items.ForEach(c => c.Children = GetChildComment(c.Id));
        return comments;
    }

    private List<CommentResponseDto> GetChildComment(int? parentId = null)
    {
        var comments = context.Comments
            .Where(c => c.comment_id == parentId)
            .Include(c => c.User)
            .Select(c => new CommentResponseDto()
            {
                Id = c.Id,
                User = new UserResponseDTO()
                {
                    Id = c.User.Id,
                    Name = c.User.Name,
                    Email = c.User.Email
                },
                Content = c.Content,
                ParentCommentId = c.comment_id,
                CreatedAt = c.CreatedAt
            })
            .ToList();
        
        foreach (var comment in comments)
        {
            comment.Children = GetChildComment(comment.Id);
        }

        return comments;
    }
    
    public CommentResponseDto CreateComment(CommentRequestDto comment, int userId)
    {
        var commentEntity = new Comment()
        {
            User = context.Users.Find(userId),
            Content = comment.Content,
            post_id = comment.PostId,
            comment_id = comment.ParentId
        };
        context.Comments.Add(commentEntity);
        context.SaveChanges();
        return new CommentResponseDto()
        {
            Id = commentEntity.Id,
            User = new UserResponseDTO()
            {
                Id = commentEntity.User.Id,
                Name = commentEntity.User.Name,
                Email = commentEntity.User.Email
            },
            Content = commentEntity.Content,
            ParentCommentId = commentEntity.comment_id,
            CreatedAt = commentEntity.CreatedAt
        };
    }
}