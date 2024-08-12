using AspNetCoreRestfulApi.Core.CustomException;
using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Entity;
using AspNetCoreRestfulApi.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AspNetCoreRestfulApi.Data;

namespace AspNetCoreRestfulApi.Services.Ipml
{
    public class BlogService(AppDbContext context) : IBlogService
    {
        public void Delete(int id)
        {
            context.Blogs.Remove(context.Blogs.Find(id)?? throw new HttpResponseException((int)HttpStatusCode.NotFound,"Not found"));
            context.SaveChanges();
        }

        public Pageable<BlogResponseDto> GetAll(int page, int size)
        {
            return context.Blogs
              .Select(u => new BlogResponseDto
              {
                  Id = u.Id,
                  Title = u.Title,
                  Content = u.Content,
                  User = new UserResponseDTO { Id = u.User.Id, Name = u.User.Name, Email = u.User.Email }
              }).ToPageable(page, size);
        }

        public Pageable<BlogResponseDto> GetBlogsByUserId(int userId, int page, int size)
        {
          
        return  context.Blogs
              .Where(b => b.User.Id == userId)
              .Select(u => new BlogResponseDto
              {
                  Id = u.Id,
                  Content = u.Content,
                  User = new UserResponseDTO { Id = u.User.Id, Name = u.User.Name, Email = u.User.Email }
              }).ToPageable(page, size);
        }

        public BlogResponseDto CreateBlog(BlogRequestDto entity, int userId)
        {
            var user = context.Users.Find(userId)?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            var blog = new Blog
            {
                Content = entity.Content,
                Title = entity.Title,
                User = user
            };
            context.Blogs.Add(blog);
            context.SaveChanges();
            return new BlogResponseDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                User = new UserResponseDTO { Id = blog.User.Id, Name = blog.User.Name, Email = blog.User.Email }
            };
        }

        public BlogResponseDto GetById(int id)
        {
           var blog = context.Blogs.Where(b => b.Id == id)
                .Include(b => b.User)
                .FirstOrDefault()??
                throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            return new BlogResponseDto
            {
                Id = blog.Id,
                Content = blog.Content,
                User = new UserResponseDTO { Id = blog.User.Id, Name = blog.User.Name, Email = blog.User.Email }
            };
        }

        public BlogResponseDto Create(BlogRequestDto entity)
        {
            throw new NotImplementedException();
        }
        
        public BlogResponseDto Update(int id, BlogRequestDto entity)
        {
            var blog = context.Blogs.Where(blog => blog.Id == id)
                .Include(blog => blog.User)
                .FirstOrDefault()?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            blog.Content = entity.Content;
            blog.Title = entity.Title;
            context.SaveChanges();
            return new BlogResponseDto
            {
                Id = blog.Id,
                Content = blog.Content,
                Title = blog.Title,
                User = new UserResponseDTO { Id = blog.User.Id, Name = blog.User.Name, Email = blog.User.Email }
            };

        }
    }
}
