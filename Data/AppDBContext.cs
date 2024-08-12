using AspNetCoreRestfulApi.Entities;
using AspNetCoreRestfulApi.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRestfulApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<User, IdentityRole<int>,int>(options)
    {
        // Maping entity to table
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        
        public DbSet<TokenBlackList> TokenBlackLists { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<GlobalChat> GlobalChats { get; set; }
    }
}
