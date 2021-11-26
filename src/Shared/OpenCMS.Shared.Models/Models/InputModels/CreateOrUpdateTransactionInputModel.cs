using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Shared.Models.InputModels
{
    public class CreateOrUpdateTransactionInputModel
    {
        public TransactionModel SalesModel { get; set; }
        public List<TransactionItemModel> SalesItemModels { get; set; }
    }
}
