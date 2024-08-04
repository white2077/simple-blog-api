using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetCoreRestfulApi.Core.CoreEntity;
using AspNetCoreRestfulApi.Entities;

namespace AspNetCoreRestfulApi.Entity
{
    [Table("blog")]
    public class Blog : BaseEntity
    {
        [Column("title")]
        [Required]
        public string Title { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }
        
        List<Post> Posts { get; set; }

        public Blog()
        {
            
        }

        public Blog(string title, string content, User user)
        {
            Title = title;
            Content = content;
            User = user;
        }
    }
}
