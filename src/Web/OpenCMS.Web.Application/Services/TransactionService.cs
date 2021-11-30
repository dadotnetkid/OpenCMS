using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenCMS.Shared.Common;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IOpenCMSHttpClient _openCmsHttpClient;

        public TransactionService(IOpenCMSHttpClient openCMSHttpClient)
        {
            _openCmsHttpClient = openCMSHttpClient;
        }
        public async Task<BaseResponse> Get(TransactionType transactionType = TransactionType.Sales,
            TransactionStatus transactionStatus = TransactionStatus.Quotation)
        {
            var http = _openCmsHttpClient.Create();
            var get = await http.GetAsync("transactions/" + (int)transactionType + "/" + (int)transactionStatus);
            var content = await get.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BaseResponse>(content);
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<BaseResponse<List<TransactionModel>>>(content);
            return JsonSerializer.Deserialize<ErrorResponse>(content);

        }

        public async Task<BaseResponse> GetById(int transactionId, TransactionType transactionType)
        {
            var http = _openCmsHttpClient.Create();
            var get = await http.GetAsync($"transactions/{transactionId}/details");
            var content = await get.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BaseResponse>(content);
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<BaseResponse<TransactionModel>>(content);
            return JsonSerializer.Deserialize<ErrorResponse>(content);
        }

        public async Task<BaseResponse> GetSalesItems(int transactionId)
        {
            var http = _openCmsHttpClient.Create();
            var get = await http.GetAsync($"transactions/{transactionId}/transactionItems");
            var content = await get.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BaseResponse>(content);
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<BaseResponse<List<TransactionItemModel>>>(content);
            }
            return JsonSerializer.Deserialize<ErrorResponse>(content);
        }

        public async Task<BaseResponse> DeleteSalesItems(int saleId, int salesItemId)
        {
            var http = _openCmsHttpClient.Create();
            var get = await http.DeleteAsync($"transactions/{saleId}/transactionItems/{salesItemId}");
            var content = await get.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BaseResponse>(content);
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<BaseResponse<List<TransactionItemModel>>>(content);
            }
            return JsonSerializer.Deserialize<ErrorResponse>(content);
        }

        public async Task<BaseResponse> CreateOrUpdate(TransactionModel salesModel,
            ObservableCollection<TransactionItemModel> salesItemModels)
        {

            var http = _openCmsHttpClient.Create();
            var model = new
            {
                salesModel,
                salesItemModels = salesItemModels.Select(x => new { x.Id, x.DiscountId, x.CreatedBy, x.Deleted, SalesId = x.TransactionId, x.CatalogId, x.Quantity, x.SubTotal, x.IsModified, x.IsNew })
            };
            var stringContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var createOrUpdate = salesModel.Id == 0 ? await http.PostAsync($"transactions", stringContent) : await http.PatchAsync("transactions", stringContent);
            var content = await createOrUpdate.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BaseResponse>(content);
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<BaseResponse>(content);
            }
            return JsonSerializer.Deserialize<ErrorResponse>(content);
        }

        public async Task<BaseResponse> Delete(int transactionId)
        {
            var http = _openCmsHttpClient.Create();
            var get = await http.DeleteAsync($"transactions/{transactionId}");
            var content = await get.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BaseResponse>(content);
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<BaseResponse<List<TransactionModel>>>(content);
            }
            return JsonSerializer.Deserialize<ErrorResponse>(content);
        }
    }
}
