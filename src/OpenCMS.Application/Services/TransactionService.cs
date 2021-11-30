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
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transactions, int> _transactionRepo;
        private readonly IRepository<TransactionItems, int> _transactionItemsRepo;
        private readonly IMapper _mapper;

        public TransactionService(IRepository<Transactions, int> transactionRepo, IRepository<TransactionItems, int> transactionItemsRepo, IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _transactionItemsRepo = transactionItemsRepo;
            _mapper = mapper;
        }
        public async Task<TransactionModel> CreateOrUpdate(CreateOrUpdateTransactionInputModel inputModel)
        {
            if (inputModel.SalesModel.Id == 0)
            {
                inputModel = await CreateTransaction(inputModel);
            }
            else
            {
                inputModel = await UpdateTransaction(inputModel);
            }
            return inputModel.SalesModel;
        }

        private async Task<CreateOrUpdateTransactionInputModel> UpdateTransaction(CreateOrUpdateTransactionInputModel inputModel)
        {
            try
            {
                foreach (var i in inputModel.SalesItemModels)
                {

                    i.TransactionId = inputModel.SalesModel.Id;
                    var item = _mapper.Map<TransactionItems>(i);
                    if (i.IsNew)
                    {
                        await _transactionItemsRepo.Insert(item);
                    }
                    else if (i.IsModified)
                    {

                        await _transactionItemsRepo.Update(item);
                    }
                }
                inputModel.SalesModel.CardFile = null;
                var sales = _mapper.Map<Transactions>(inputModel.SalesModel);
                await _transactionRepo.Update(sales);
                return inputModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<CreateOrUpdateTransactionInputModel> CreateTransaction(CreateOrUpdateTransactionInputModel inputModel)
        {

            var sales = _mapper.Map<Transactions>(inputModel.SalesModel);
            sales.DateCreated = DateTime.Now;
            long maxTransaction = 0;
            if (_transactionRepo.Fetch().Any())
                maxTransaction = _transactionRepo.Fetch().Max(x => x.TransactionNumber);
            sales.TransactionNumber = maxTransaction + 1;
            sales = await _transactionRepo.Insert(sales);
            foreach (var i in inputModel.SalesItemModels)
            {
                i.TransactionId = sales.Id;
                var item = _mapper.Map<TransactionItems>(i);
                await _transactionItemsRepo.Insert(item);

            }
            return inputModel;
        }
    }
}
