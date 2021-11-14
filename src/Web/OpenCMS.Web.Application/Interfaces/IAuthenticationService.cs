using System.Threading.Tasks;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<LoginResult> Login(string userName, string password);
        Task Logout();
    }
}
