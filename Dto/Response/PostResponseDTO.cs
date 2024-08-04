namespace AspNetCoreRestfulApi.Dto.Response
{
    public class PostResponseDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public BlogResponseDTO Blog { get; set; }

        public PostResponseDTO(string title, string content, BlogResponseDTO blog)
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
