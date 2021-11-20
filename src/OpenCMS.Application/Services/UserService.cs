using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;
using OpenCMS.Domain.Models;
using OpenCMS.Shared.Models.Models;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users, string> _userRepo;
        private readonly IRepository<Roles, string> _roleRepo;
        private readonly IRepository<Permissions, int> _permissionRepo;
        private readonly IRepository<PermissionsInRoles, int> _permissionInRolesRepo;
        private readonly OpenCMSDb _db;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IRepository<Users, string> userRepo,
            IRepository<Roles, string> roleRepo,
            IRepository<Permissions, int> permissionRepo,
            IRepository<PermissionsInRoles, int> permissionInRolesRepo,
            OpenCMSDb db, IPasswordHasher<Users> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _permissionRepo = permissionRepo;
            _permissionInRolesRepo = permissionInRolesRepo;
            _db = db;
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

     

        public async Task<Users> Create(Users user, string password, RoleModel[] itemUserRoles)
        {
            var hash = _passwordHasher.HashPassword(user, password);
            user.PasswordHash = hash;
            user.Id = Guid.NewGuid().ToString();
            if (itemUserRoles.Any())
            {
                var roles = new List<Roles>();
                foreach (var i in itemUserRoles)
                {
                    roles.Add(_db.Roles.Find(i.Id));
                }
                user.Roles = roles;
            }
            await _userRepo.Insert(user);

            return user;
        }

        public async Task<Users> Update(Users user, string itemPassword,RoleModel[] itemUserRoles)
        {
            user = _userRepo.Find(x => x.Id == user.Id);
            if (!string.IsNullOrEmpty(itemPassword))
            {
                //updatePasswordhere
                user.PasswordHash = _passwordHasher.HashPassword(user, itemPassword);
            }
            await _userRepo.Update(user);
            if (itemUserRoles.Any())
            {
                //update role here
                await UpdateUsersRole(user, itemUserRoles);
            }


            return user;
        }

        public async Task<Roles> CreateRole(Roles role)
        {
            role.Id = Guid.NewGuid().ToString();
            role = await _roleRepo.Insert(role);
            foreach (var i in _permissionRepo.GetAll())
            {
                await _permissionInRolesRepo.Insert(new PermissionsInRoles()
                {
                    PermissionId = i.Id,
                    RoleId = role.Id,
                    CanAdd = false,
                    CanDelete = false,
                    CanUpdate = false,
                    CanView = false

                });
            }

            return role;
        }

        private async Task UpdateUsersRole(Users user, RoleModel[] itemUserRoles)
        {
            user = _db.Users.Include("Roles").FirstOrDefault(x => x.Id == user.Id);
            user.Roles.Clear();
            foreach (var i in itemUserRoles)
            {
                user.Roles.Add(_roleRepo.Find(i.Id));
            }

            await _db.SaveChangesAsync();
        }


    }
}
