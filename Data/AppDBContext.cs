﻿using AspNetCoreRestfulApi.Entities;
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
        public DbSet<User> User { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
