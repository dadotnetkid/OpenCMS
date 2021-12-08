using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Infrastructure.Models;
using Refit;

namespace OpenCMS.Web.Application.ApiClients
{
    public interface IAccountsApiClient
    {
        [Post("/Accounts/{classificationId}")]
        public Task<BaseResponse<List<AccountsModel>>> GetAll(string classificationId = "");
        [Get("/Accounts/GetAllClassification")]
        public Task<BaseResponse<List<ClassificationModel>>> GetAllClassification();
    }
}
