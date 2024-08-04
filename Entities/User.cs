using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetCoreRestfulApi.Core.CoreEntity;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreRestfulApi.Entity
{
    [Table("User")]
    public class User : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public ICollection<Blog> Blogs { get; set; }

        public User(string name, string email):base()
        {
            Name = name;
            Email = email;
        }
        public User():base()
        {
            
        }
    }
}
