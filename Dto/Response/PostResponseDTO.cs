namespace AspNetCoreRestfulApi.Dto.Response
{
    public class PostResponseDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public BlogResponseDto Blog { get; set; }

        public PostResponseDTO(string title, string content, BlogResponseDto blog)
        {
            Title = title;
            Content = content;
            Blog = blog;
        }
        public PostResponseDTO()
        {
            
        }

    }
}
