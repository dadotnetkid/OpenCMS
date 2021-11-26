using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;
using OpenCMS.Infrastructure.Common;
using OpenCMS.Shared.Common;
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Models.InputModels;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController, Guard, Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly IRepository<Transactions, int> _transactionRepo;
        private readonly IRepository<TransactionItems, int> _transactionItemsRepo;
        private readonly ISalesService _salesService;
        private readonly IMapper _mapper;

        public TransactionsController(IRepository<Transactions, int> transactionRepo, IRepository<TransactionItems, int> transactionItemsRepo, ISalesService salesService, IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _transactionItemsRepo = transactionItemsRepo;
            _salesService = salesService;
            _mapper = mapper;
        }
        [HttpGet("{transactionTypes}")]
        public IActionResult Get(TransactionTypes transactionTypes)
        {
            var model = _mapper.ProjectTo<TransactionModel>(_transactionRepo.Fetch(x=>x.TransactionType==(int)transactionTypes, includeProperties: "CardFile"));
            return Ok(new BaseResponse<object>()
            {
                Data = model,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpGet("{transactionId}/details")]
        public IActionResult GetDetails(int transactionId)
        {
            var model = _mapper.Map<TransactionModel>(_transactionRepo.Find(x => x.Id == transactionId, includeProperties: "CardFile"));
            return Ok(new BaseResponse<object>()
            {
                Data = model,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpGet("{transactionId}/transactionItems")]
        public IActionResult GetTransactionItems(int transactionId)
        {
            var model = _mapper.ProjectTo<TransactionItemModel>(_transactionItemsRepo.Fetch(x => x.SalesId == transactionId, includeProperties: "Catalogs"));
            return Ok(new BaseResponse<object>()
            {
                Data = model,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpDelete("{transactionId}/transactionItems/{transactionItemId}")]
        public async Task<IActionResult> DeleteTransactionItems(int transactionId, int transactionItemId)
        {
            await _transactionItemsRepo.Delete(transactionItemId);
            var model = _mapper.ProjectTo<TransactionItemModel>(_transactionItemsRepo.Fetch(x => x.SalesId == transactionId, includeProperties: "Catalogs"));
            return Ok(new BaseResponse<object>()
            {
                Data = model,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateTransactionInputModel item)
        {
            await _salesService.CreateOrUpdate(item);
            return Ok(new BaseResponse<object>()
            {
                Data = item.SalesModel,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateTransactionInputModel item)
        {
            await _salesService.CreateOrUpdate(item);
            return Ok(new BaseResponse<object>()
            {
                Data = item.SalesModel,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
