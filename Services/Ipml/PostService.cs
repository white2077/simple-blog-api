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
    public class PostService(AppDbContext context) : IPostService
    {
        public PostResponseDTO Create(PostRequestDto entity)
        {
            var post = new Post
            {
                Title = entity.Title,
                Content = entity.Content,
                Blog = context.Blogs
                .Where(b => b.Id == entity.BlogId)
                .Include(b => b.User)
                .FirstOrDefault()??throw new HttpResponseException((int)HttpStatusCode.NotFound, "User Not found"),
                CreatedAt = DateTime.Now,
            };

            context.Posts.Add(post);
            context.SaveChanges();
            return new PostResponseDTO
            {
                Title = post.Title,
                Content = post.Content,
                Blog = new BlogResponseDto
                {
                    Id = post.Blog.Id,
                    Content = post.Blog.Content,
                    User = new UserResponseDTO
                    {
                        Id = post.Blog.User.Id,
                        Name = post.Blog.User.Name,
                        Email = post.Blog.User.Email
                    }
                }
            };
        }

        public void Delete(int id)
        {
            context.Posts.Remove(context.Posts.Find(id)?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found"));
        }

        public Pageable<PostResponseDTO> GetAll(int page, int size)
        {
           return context.Posts.Select(p => new PostResponseDTO
           {
               Title = p.Title,
               Content = p.Content,
               Blog = new BlogResponseDto
               {
                   Id = p.Blog.Id,
                   Content = p.Blog.Content,
                   User = new UserResponseDTO
                   {
                       Id = p.Blog.User.Id,
                       Name = p.Blog.User.Name,
                       Email = p.Blog.User.Email
                   }
               }
           }).ToPageable(page, size);
        }

        public PostResponseDTO GetById(int id)
        {
           return  context.Posts.Where(p => p.Id == id)
                .Include(p => p.Blog)
                .ThenInclude(b => b.User)
                .Select(p => new PostResponseDTO
            {
                    Title = p.Title,
                    Content = p.Content,
                    Blog = new BlogResponseDto
                    {
                        Id = p.Blog.Id,
                        Content = p.Blog.Content,
                        User = new UserResponseDTO
                        {
                            Id = p.Blog.User.Id,
                            Name = p.Blog.User.Name,
                            Email = p.Blog.User.Email
                        }
                    }
            }).FirstOrDefault() ?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
        }

        public PostResponseDTO Update(int id, PostRequestDto entity)
        {
            var post = context.Posts.Find(id)?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            var blog = context.Blogs.Find(entity.BlogId)?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            post.Title = entity.Title;
            post.Content = entity.Content;
            post.Blog = blog;
            context.SaveChanges();
            return new PostResponseDTO
            {
                Title = post.Title,
                Content = post.Content,
                Blog = new BlogResponseDto
                {
                    Id = post.Blog.Id,
                    Content = post.Blog.Content,
                    User = new UserResponseDTO
                    {
                        Id = post.Blog.User.Id,
                        Name = post.Blog.User.Name,
                        Email = post.Blog.User.Email
                    }
                }
            };
        }
    }
}
