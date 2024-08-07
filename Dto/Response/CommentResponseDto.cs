namespace AspNetCoreRestfulApi.Dto.Response;

public class CommentResponseDto
{
    public int Id { get; set; }
    public UserResponseDTO User { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public int? ParentCommentId { get; set; }
    public List<CommentResponseDto> Children { get; set; }

    public CommentResponseDto(int id, UserResponseDTO user, string content, DateTime createdAt, List<CommentResponseDto> children)
    {
        Id = id;
        User = user;
        Content = content;
        CreatedAt = createdAt;
        this.Children = children;
    }

    public CommentResponseDto()
    {
    }
}