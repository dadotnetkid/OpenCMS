using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Models.InputModels;

namespace OpenCMS.Application.Interfaces.Services
{
    public interface ISalesService
    {
        public Task<TransactionModel> CreateOrUpdate(CreateOrUpdateTransactionInputModel inputModel);
    }
}
