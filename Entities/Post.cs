using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetCoreRestfulApi.Core.CoreEntity;
using AspNetCoreRestfulApi.Entities;

namespace AspNetCoreRestfulApi.Entity
{
    [Table("Post")]
    public class Post : BaseEntity
    {
        [Required]
        [Column("title",TypeName ="NVARCHAR(255)")]
        public string Title { get; set; }
        [Required]
        [Column("content",TypeName ="TEXT")]
        public string Content { get; set; }

        [ForeignKey("blog_id")]
        public Blog Blog { get; set; }
        
        [ForeignKey("user_id")]
        public User User { get; set; }

        public Post(string title, string content, Blog blog)
        {
            Title = title;
            Content = content;
            Blog = blog;
        }

        public Post()
        {
            
        }

    }
}
