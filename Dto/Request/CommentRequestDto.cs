namespace AspNetCoreRestfulApi.Dto.Request;

public class CommentRequestDto
{
    public int PostId { get; set; }
    public string Content { get; set; }
    public int? ParentId { get; set; }
    
    public CommentRequestDto(int postId, string content, int parentId)
    {
        this.PostId = postId;
        this.Content = content;
        this.ParentId = parentId;
    }

    public CommentRequestDto()
    {
    }
}