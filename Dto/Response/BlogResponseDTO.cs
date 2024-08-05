namespace AspNetCoreRestfulApi.Dto.Response
{
    public class BlogResponseDto
    {
        public int Id { get; set; }
        
        public String Content { get; set; }

        public UserResponseDTO User { get; set; }

        public BlogResponseDto()
        {
        }

        public BlogResponseDto(int id, String content, UserResponseDTO user)
        {
            Id = id;
            Content = content;
            User = user;
        }

    }
}
