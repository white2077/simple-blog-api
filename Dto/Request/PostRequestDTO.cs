namespace AspNetCoreRestfulApi.Dto.Request
{
    public class PostRequestDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        public PostRequestDTO(string title, string content, int blogId)
        {
            Title = title;
            Content = content;
            BlogId = blogId;
        }
        public PostRequestDTO()
        {
            
        }
    }
}
