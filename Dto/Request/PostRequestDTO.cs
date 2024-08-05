namespace AspNetCoreRestfulApi.Dto.Request
{
    public class PostRequestDto(string title, string content, int blogId)
    {
        public string Title { get; set; } = title;
        public string Content { get; set; } = content;
        public int BlogId { get; set; } = blogId;
    }
}
