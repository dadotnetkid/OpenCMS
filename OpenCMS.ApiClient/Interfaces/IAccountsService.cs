using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Infrastructure.Models;
using Refit;

namespace OpenCMS.ApiClient.Interfaces
{
    public interface IAccountsService
    {
        [Post("/Accounts/{classificationId}")]
        public Task<BaseResponse<List<AccountsModel>>> GetAll(string classificationId = "");
        [Get("/Accounts/GetAllClassification")]
        public Task<BaseResponse<List<ClassificationModel>>> GetAllClassification();
    }
}
