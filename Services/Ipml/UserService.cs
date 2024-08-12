using AspNetCoreRestfulApi.Core.CustomException;
using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Entity;
using AspNetCoreRestfulApi.Utils;
using System.Net;
using AspNetCoreRestfulApi.Data;
using AspNetCoreRestfulApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreRestfulApi.Services.Ipml
{
    public class UserService(AppDbContext context,SignInManager<User> signInManager) : IUserService
    {
        public  Pageable<UserResponseDTO> GetAll(int page, int size)
        {
            Pageable<UserResponseDTO>  pageable = context.Users
              .Select(u => new UserResponseDTO
              {
                  Id = u.Id,
                  Name = u.Name,
                  Email = u.Email
              }).ToPageable(page, size);

            return null;
        }

        public  UserResponseDTO GetById(int id)
        {
            var user =  context.Users.Find(id)??throw new HttpResponseException(404,"That entity not found for abc =)))) ");

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        public  UserResponseDTO Create(UserRequestDto entity)
        {
            var user = new User
            {
                Name = entity.Name,
                Email = entity.Email,
            };

            context.Users.Add(user);
            context.SaveChanges();

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        public UserResponseDTO Update(int id, UserRequestDto entity)
        {
           var user = context.Users
               .FirstOrDefault(u => u.Id == id) ?? 
                      throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            user.Name = entity.Name;
            user.Email = entity.Email;
            user.UpdatedAt = DateTime.Now;
            context.SaveChanges();
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        public void Delete(int id)
        {
            context.Users.Remove(context.Users.Find(id)?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found"));
        }

        public BlogResponseDto CreateBlog(BlogRequestDto entity, int userId)
        {
            var user = context.Users.Find(userId) ?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found");
            var role =signInManager.UserManager.GetRolesAsync(user).Result[0];
           
            var blog = new Blog
            {
                Title = entity.Title,
                Content = entity.Content,
                User = user
            };
            
            context.Blogs.Add(blog);
            context.SaveChanges();
            
            return new BlogResponseDto()
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                User = new UserResponseDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = role
                }
            };
        }
    }
}
