using System.ComponentModel.DataAnnotations;

namespace AspNetCoreRestfulApi.Dto.Response
{
    public class BlogResponseDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Title must be less than 100")]
        [MinLength(5, ErrorMessage = "Title must be greater than 5")]
        public string Title { get; set; }
        
        public String Content { get; set; }

        public UserResponseDTO User { get; set; }

        public BlogResponseDto()
        {
        }

        public BlogResponseDto(int id, String content, UserResponseDTO user, string title)
        {
            Id = id;
            Content = content;
            User = user;
            Title = title;
        }

    }
}
