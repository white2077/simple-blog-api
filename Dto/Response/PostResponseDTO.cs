namespace AspNetCoreRestfulApi.Dto.Response
{
    public class PostResponseDTO
    {
        public int Id{ get; set; }
        
        public string Title { get; set; }
        public string Content { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public BlogResponseDto Blog { get; set; }

        public PostResponseDTO(string title, string content, BlogResponseDto blog,DateTime createdAt,int id)
        {
            Id = id;
            Title = title;
            Content = content;
            Blog = blog;
            CreatedAt = createdAt;
        }
        public PostResponseDTO()
        {
            
        }

    }
}
