using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using OpenCMS.Domain.Models;
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Models.Models;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface IUserService
    {
        public Task<PaginatedBaseItems<List<UserModel>>> GetAll();
        public string GetUserId();
        public List<string> GetUserRoles();
        public Task<List<RoleModel>> GetRoles();
        public bool GetUserIsRoles(params string[] roles);
        Task Initialize();
        public TokenResponse TokenResponse { get; set; }
        public bool IsAuthenticated();
        
        public string GetUserName();  
        public string GetGivenName();
        public Task<BaseResponse> UpdateUser(UserModel item);
        public Task<BaseResponse> CreateUser(UserModel item);
        public Task<BaseResponse> CreateRole(RoleModel item);
        public Task<BaseResponse> GetPermissionsInRoles(string roleId);
    }
}
