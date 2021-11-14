using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using OpenCMS.Domain.Models;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface IUserService
    {
        public Task<PaginatedBaseItems<List<UserModel>>> GetAll();
        public string GetUserId();
        public List<string> GetUserRoles();
        public bool GetUserIsRoles(params string[] roles);
        Task Initialize();
        public TokenResponse TokenResponse { get; set; }
        public bool IsAuthenticated();
        public Task<UserModel> CreateUser(UserModel item);
        public string GetUserName();
    }
}
