using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users, string> _userRepo;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IRepository<Users, string> userRepo, IPasswordHasher<Users> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<string> GetUserRoles()
        {
            var res = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value);
            return res.ToList();
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<Users> GetUser()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Users>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Users> GetById(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> Create(Users user, string password)
        {
            var hash = _passwordHasher.HashPassword(user, password);
            user.PasswordHash = hash;
            user.Id = Guid.NewGuid().ToString();
            await _userRepo.Insert(user);
            return user;
        }
    }
}
