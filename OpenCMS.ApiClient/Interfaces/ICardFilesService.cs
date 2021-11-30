using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCMS.Shared.Common;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Infrastructure.Models;
using Refit;

namespace OpenCMS.ApiClient.Interfaces
{
    public interface ICardFilesService
    {
        [Get("/cardFiles/{cardFileType}")]
        public Task<PaginatedBaseResponse<List<CardFilesModel>>> GetAll(int cardFileType);
        [Delete("/cardFiles/{cardFileId}")]
        Task Delete(int cardFileId);
        Task<BaseResponse> CreateOrUpdate(CardFilesModel model);
    }
}
