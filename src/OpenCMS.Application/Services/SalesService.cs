using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Models.InputModels;

namespace OpenCMS.Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly IRepository<Transactions, int> _salesRepo;
        private readonly IRepository<TransactionItems, int> _salesItemsRepo;
        private readonly IMapper _mapper;

        public SalesService(IRepository<Transactions, int> salesRepo, IRepository<TransactionItems, int> salesItemsRepo, IMapper mapper)
        {
            _salesRepo = salesRepo;
            _salesItemsRepo = salesItemsRepo;
            _mapper = mapper;
        }
        public async Task<TransactionModel> CreateOrUpdate(CreateOrUpdateTransactionInputModel inputModel)
        {
            if (inputModel.SalesModel.Id == 0)
            {
                inputModel = await CreateSales(inputModel);
            }
            else
            {
                inputModel = await UpdateSales(inputModel);
            }
            return inputModel.SalesModel;
        }

        private async Task<CreateOrUpdateTransactionInputModel> UpdateSales(CreateOrUpdateTransactionInputModel inputModel)
        {
            try
            {
                foreach (var i in inputModel.SalesItemModels)
                {

                    i.SalesId = inputModel.SalesModel.Id;
                    var item = _mapper.Map<TransactionItems>(i);
                    if (i.IsNew)
                    {
                        await _salesItemsRepo.Insert(item);
                    }
                    else if (i.IsModified)
                    {
                     
                        await _salesItemsRepo.Update(item);
                    }
                }
                inputModel.SalesModel.CardFile = null;
                var sales = _mapper.Map<Transactions>(inputModel.SalesModel);
                await _salesRepo.Update(sales);
                return inputModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<CreateOrUpdateTransactionInputModel> CreateSales(CreateOrUpdateTransactionInputModel inputModel)
        {

            var sales = _mapper.Map<Transactions>(inputModel.SalesModel);
            sales.DateCreated = DateTime.Now;
            sales = await _salesRepo.Insert(sales);
            foreach (var i in inputModel.SalesItemModels)
            {
                i.SalesId = sales.Id;
                var item = _mapper.Map<TransactionItems>(i);
                await _salesItemsRepo.Insert(item);

            }
            return inputModel;
        }
    }
}
