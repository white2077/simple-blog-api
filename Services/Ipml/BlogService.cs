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
        public BlogResponseDTO Create(BlogRequestDTO entity)
        {
            var user = context.User.Find(entity.UserId) ?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            var blog = new Blog
            {
                Content = entity.Content,
                Title = entity.Title,
                User = user,
            };
            context.Blogs.Add(blog);
            context.SaveChanges();
            return new BlogResponseDTO
            {
                Id = blog.Id,
                Content = blog.Content,
                User = new UserResponseDTO { Id = blog.User.Id, Name = blog.User.Name, Email = blog.User.Email }
            };
        }

        public void Delete(int id)
        {
            context.Blogs.Remove(context.Blogs.Find(id)?? throw new HttpResponseException((int)HttpStatusCode.NotFound,"Not found"));
        }

        public Pageable<BlogResponseDTO> GetAll(int page, int size)
        {
            return context.Blogs
              .Select(u => new BlogResponseDTO
              {
                  Id = u.Id,
                  Content = u.Content,
                  User = new UserResponseDTO { Id = u.User.Id, Name = u.User.Name, Email = u.User.Email }
              }).ToPageable(page, size);
        }

        public Pageable<BlogResponseDTO> getBlogsByUserId(int userId, int page, int size)
        {
          
        return  context.Blogs
              .Where(b => b.User.Id == userId)
              .Select(u => new BlogResponseDTO
              {
                  Id = u.Id,
                  Content = u.Content,
                  User = new UserResponseDTO { Id = u.User.Id, Name = u.User.Name, Email = u.User.Email }
              }).ToPageable(page, size);
        }

        public BlogResponseDTO GetById(int id)
        {
           var blog = context.Blogs.Where(b => b.Id == id)
                .Include(b => b.User)
                .FirstOrDefault()??
                throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            return new BlogResponseDTO
            {
                Id = blog.Id,
                Content = blog.Content,
                User = new UserResponseDTO { Id = blog.User.Id, Name = blog.User.Name, Email = blog.User.Email }
            };
        }

        public BlogResponseDTO Update(int id, BlogRequestDTO entity)
        {
            var User = context.User.Find(entity.UserId)??
               throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found"); 
            var Blog = context.Blogs.Find(id)?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            Blog.Content = entity.Content;
            Blog.Title = entity.Title;
            Blog.User = User;
            context.SaveChanges();
            return new BlogResponseDTO
            {
                Id = Blog.Id,
                Content = Blog.Content,
                User = new UserResponseDTO { Id = Blog.User.Id, Name = Blog.User.Name, Email = Blog.User.Email }
            };

        }
    }
}
