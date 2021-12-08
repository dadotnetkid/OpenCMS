using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCMS.Shared.Models;
using Refit;

namespace OpenCMS.Web.Application.ApiClients
{
    public interface ICatalogsApiClient
    {
        [Get("/catalogs")]
        public Task<PaginatedBaseResponse<List<CatalogModel>>> GetAll();
        [Delete("/catalogs/{contextId}")]
        Task Delete(int contextId);
        [Post("/catalogs")]
        Task<BaseResponse> Create([Body] CatalogModel model);
        [Patch("/catalogs")]
        Task<BaseResponse> Update([Body] CatalogModel model);
        [Post("/catalogs/{catalogId}/buying-details")]
        Task<BaseResponse> CreateBuyingDetails(int? catalogId,[Body] CatalogBuyingDetailsModel model);
        [Patch("/catalogs/{catalogId}/buying-details")]
        Task<BaseResponse> UpdateBuyingDetails(int? catalogId,[Body] CatalogBuyingDetailsModel model);
    }
}
