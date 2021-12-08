using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCMS.Shared.Models;
using Refit;

namespace OpenCMS.Web.Application.ApiClients
{
    public interface ICardFilesApiClient
    {
        [Get("/cardFiles/{cardFileType}")]
        public Task<PaginatedBaseResponse<List<CardFilesModel>>> GetAll(int cardFileType);
        [Delete("/cardFiles/{cardFileId}")]
        Task<BaseResponse> Delete(int cardFileId);
        Task<BaseResponse> CreateOrUpdate(CardFilesModel model);
    }
}
