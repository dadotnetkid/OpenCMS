using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenCMS.Application.EventHandlers;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;
using OpenCMS.Domain.Models.InputModels;
using OpenCMS.Infrastructure.Common;
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Models.InputModels;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly IRepository<Catalogs, int> _catalogRepo;
        private readonly IRepository<CatalogBuyingDetails, int> _buyingRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CatalogsController(IRepository<Catalogs, int> catalogRepo, IRepository<CatalogBuyingDetails, int> buyingRepo,
            IMapper mapper,
            IMediator mediator)
        {
            _catalogRepo = catalogRepo;
            _buyingRepo = buyingRepo;
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult Get()
        {

            var catalog = _mapper.ProjectTo<CatalogModel>(_catalogRepo.Fetch(includeProperties: "CatalogBuyingDetails,CatalogSellingDetails"));
            return Ok(new PaginatedBaseResponse<object>()
            {
                Data = new PaginatedBaseItems<object>()
                {
                    Items = catalog,
                    Total = _catalogRepo.Fetch().Select(x => x.Id).Count()
                },
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CatalogModel model)
        {
            var item = _mapper.Map<Catalogs>(model);
            /* new Catalogs()
             {
                 Name = item.Name,
                 ItemNumber = item.ItemNumber,
                 IBuyThisItem = item.IBuyThisItem,
                 ISellThisItem = item.ISellThisItem,
                 IInventoryThisItem = item.IInventoryThisItem,
                 SalesAcount = item.,
                 IncomeAcount = item.IncomeAccount,
                 InventoryAccount = item.InventoryAccount,
                 ManufacturerNo = item.ManufacturerNo,
                 Description = item.Description
             })*/
            await _catalogRepo.Insert(item);
            return Ok(new PaginatedBaseResponse<object>()
            {
                Data = new PaginatedBaseItems<object>()
                {
                    Items = _catalogRepo.Fetch().ToList(),
                    Total = _catalogRepo.Fetch().Select(x => x.Id).Count()
                },
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateAsync([FromBody] CatalogModel model)
        {
            var item = _mapper.Map<Catalogs>(model);
            await _catalogRepo.Update(item);
            return Ok(new PaginatedBaseResponse<object>()
            {
                Data = new PaginatedBaseItems<object>()
                {
                    Items = _catalogRepo.Fetch().ToList(),
                    Total = _catalogRepo.Fetch().Select(x => x.Id).Count()
                },
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpDelete("{catalogId}")]
        public async Task<IActionResult> DeleteAsync(int catalogId)
        {
            await _catalogRepo.Delete(catalogId);
            return Ok(new PaginatedBaseResponse<object>()
            {
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }

        [HttpPost("{catalogId}/buying-details")]
        public async Task<IActionResult> CreateBuyingDetails([FromRoute] int catalogId,
            CatalogBuyingDetailsModel model)
        {
            try
            {

                var item = _mapper.Map<CatalogBuyingDetails>(model);
                await _buyingRepo.Insert(item);
                await _mediator.Send(new BuyingDetailsCreateOrUpdate() { CatalogBuyingDetails = item });

                return Ok(new BaseResponse()
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                });
            }
            catch (Exception e)
            {
                return Ok(new ErrorResponse(e.Message) { });
            }
        }
        [HttpPatch("{catalogId}/buying-details")]
        public async Task<IActionResult> UpdateBuyingDetails([FromRoute] int catalogId,
            CatalogBuyingDetailsModel model)
        {

            var item = _mapper.Map<CatalogBuyingDetails>(model);
            await _buyingRepo.Update(item);
            await _mediator.Send(new BuyingDetailsCreateOrUpdate() { CatalogBuyingDetails = item });
            return Ok(new PaginatedBaseResponse<object>()
            {
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
