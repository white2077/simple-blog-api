﻿using AspNetCoreRestfulApi.Core.CustomException;
using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Request;
using AspNetCoreRestfulApi.Dto.Response;
using AspNetCoreRestfulApi.Entity;
using AspNetCoreRestfulApi.Utils;
using System.Net;

namespace AspNetCoreRestfulApi.Services.Ipml
{
    public class UserService : IUserService
    {
        private readonly DBContext.AppDBContext _context;

        public UserService(DBContext.AppDBContext context)
        {
            _context = context;
        }

        public  Pageable<UserResponseDTO> GetAll(int page, int size)
        {
            return _context.Users
              .Select(u => new UserResponseDTO
              {
                  Id = u.Id,
                  Name = u.Name,
                  Email = u.Email,
              }).ToPageable(page, size);
        }

        public  UserResponseDTO GetById(int id)
        {
            var user =  _context.Users.Find(id)??throw new HttpResponseException(404,"That entity not found for abc =)))) ");

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        public  UserResponseDTO Create(UserRequestDTO entity)
        {
            var user = new User
            {
                Name = entity.Name,
                Email = entity.Email,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        public UserResponseDTO Update(int id, UserRequestDTO entity)
        {
           var user = _context.Users
                    .Where(u => u.Id == id)

                    .FirstOrDefault();
            user.Name = entity.Name;
            user.Email = entity.Email;
            user.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Find(id)?? throw new HttpResponseException((int)HttpStatusCode.NotFound, "Not found"));
        }
    }
}
