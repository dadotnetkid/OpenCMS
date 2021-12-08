using System.Threading.Tasks;
using OpenCMS.Shared.Models;
using Refit;

namespace OpenCMS.Web.Application.ApiClients
{
    
    public interface ITransactionApiClient
    {
        [Patch("/transactions/move-to-order/{transactionId}")]
        public Task<BaseResponse> MoveToOrder(int transactionId);
        [Put("/transactions/make-payment")]
        Task<BaseResponse> MakePayment(TransactionModel transactionModel, PaymentsModel paymentModel);
    }
}
