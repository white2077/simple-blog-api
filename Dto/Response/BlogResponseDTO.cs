namespace AspNetCoreRestfulApi.Dto.Response
{
    public class BlogResponseDTO
    {
        public int Id { get; set; }
        
        public String Content { get; set; }

        public UserResponseDTO User { get; set; }

        public BlogResponseDTO()
        {
        }

        public BlogResponseDTO(int id, String content, UserResponseDTO user)
        {
            Id = id;
            Content = content;
            User = user;
        }

    }
}
