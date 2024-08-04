using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetCoreRestfulApi.Entity;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreRestfulApi.Entities
{
    [Table("User")]
    public class User : IdentityUser<int>
    {
        
        [Column("Name")]
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public ICollection<Blog> Blogs { get; set; }

        public User(string name, string email):base()
        {
            Name = name;
        }
        public User():base()
        {
            
        }
    }
}
