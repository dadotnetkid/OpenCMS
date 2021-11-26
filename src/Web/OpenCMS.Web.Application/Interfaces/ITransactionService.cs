using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Shared.Common;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface ITransactionService
    {
        public Task<BaseResponse> Get(TransactionTypes transactionTypes=TransactionTypes.Sales);
        public Task<BaseResponse> GetById(int saleId, TransactionTypes transactionType);
        public Task<BaseResponse> GetSalesItems(int saleId);
        public Task<BaseResponse> DeleteSalesItems(int saleId,int salesItemId);
        public Task<BaseResponse> CreateOrUpdate(TransactionModel model, ObservableCollection<TransactionItemModel> salesItemModels);
    }
}
