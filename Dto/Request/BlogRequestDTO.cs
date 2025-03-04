﻿using System.ComponentModel.DataAnnotations;

namespace AspNetCoreRestfulApi.Dto.Request
{
    public class BlogRequestDto(string title, string content)
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5, ErrorMessage = "Tieu De Phai lon hon 5")]
        public String Title { get; set; } = title;

        public String Content { get; set; } = content;
        
    }
}
