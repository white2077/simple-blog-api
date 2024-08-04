using AspNetCoreRestfulApi.Core.CustomException;
using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.DBContext;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Entity;
using AspNetCoreRestfulApi.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AspNetCoreRestfulApi.Services.Ipml
{
    public class BlogService : IBlogService
    {

        private readonly DBContext.AppDBContext _context;


        public BlogService(AppDBContext context)
        {
            _context = context;
        }

        public BlogResponseDTO Create(BlogRequestDTO entity)
        {
            var user = _context.Users.Find(entity.UserId) ?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            var blog = new Blog
            {
                Content = entity.Content,
                Title = entity.Title,
                User = user,
            };
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return new BlogResponseDTO
            {
                Id = blog.Id,
                Content = blog.Content,
                User = new UserResponseDTO { Id = blog.User.Id, Name = blog.User.Name, Email = blog.User.Email }
            };
        }

        public void Delete(int id)
        {
            _context.Blogs.Remove(_context.Blogs.Find(id)?? throw new HttpResponseException((int)HttpStatusCode.NotFound,"Not found"));
        }

        public Pageable<BlogResponseDTO> GetAll(int page, int size)
        {
            return _context.Blogs
              .Select(u => new BlogResponseDTO
              {
                  Id = u.Id,
                  Content = u.Content,
                  User = new UserResponseDTO { Id = u.User.Id, Name = u.User.Name, Email = u.User.Email }
              }).ToPageable(page, size);
        }

        public Pageable<BlogResponseDTO> getBlogsByUserId(int userId, int page, int size)
        {
          
        return  _context.Blogs
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
           var blog = _context.Blogs.Where(b => b.Id == id)
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
            var User = _context.Users.Find(entity.UserId)??
               throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found"); 
            var Blog = _context.Blogs.Find(id)?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            Blog.Content = entity.Content;
            Blog.Title = entity.Title;
            Blog.User = User;
            _context.SaveChanges();
            return new BlogResponseDTO
            {
                Id = Blog.Id,
                Content = Blog.Content,
                User = new UserResponseDTO { Id = Blog.User.Id, Name = Blog.User.Name, Email = Blog.User.Email }
            };

        }
    }
}
