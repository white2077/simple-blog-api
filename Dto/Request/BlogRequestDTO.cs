using System.ComponentModel.DataAnnotations;

namespace AspNetCoreRestfulApi.Dto.Request
{
    public class BlogRequestDTO
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5, ErrorMessage = "Tieu De Phai lon hon 5")]
        public String Title { get; set; }

        public String Content { get; set; }

        [Required]
        public int UserId { get; set; }


        public BlogRequestDTO()
        {
        }

        public BlogRequestDTO(string title, string content, int userId)
        {
            Title = title;
            Content = content;
            UserId = userId;
        }

    }
}
