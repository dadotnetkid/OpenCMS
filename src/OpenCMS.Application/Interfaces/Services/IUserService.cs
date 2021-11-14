using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Application.Interfaces.Services
{
    public interface IUserService
    {
        public List<string> GetUserRoles();
        public string GetUserId();
        public Task<Users> GetUser();

        public Task<List<Users>> GetAll();
        public Task<Users> GetById(string userId);
        public Task<Users> Create(Users user, string password);
    }
}
