using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;
using OpenCMS.Domain.Models.InputModels;
using OpenCMS.Infrastructure.Common;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly IRepository<Catalogs, int> _catalogRepo;
        private readonly IMapper _mapper;

        public CatalogsController(IRepository<Catalogs, int> catalogRepo,IMapper mapper)
        {
            _catalogRepo = catalogRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            /*var catalog = _catalogRepo.Fetch().Select(x => new CatalogModel
            {
                Id = x.Id,
                Name = x.Name,
                ItemNumber = x.ItemNumber,
                IBuyThisItem = x.IBuyThisItem ?? false,
                IInventoryThisItem = x.IInventoryThisItem ?? false,
                ISellThisItem = x.ISellThisItem ?? false,
                IncomeAcount = x.IncomeAcount,
                InventoryAccount = x.InventoryAccount,
                SalesAcount = x.SalesAcount,
                BuyingDetails = x.CatalogBuyingDetails.Select(c => new CatalogBuyingDetailsModel()
                {
                    StandardCost=c.StandardCost,
                   UOM=c.UOM,
                   MinimumLevelRestockingAlert=c.MinimumLevelRestockingAlert,
                   DefaultReorderQuantity = c.DefaultReorderQuantity
                }).FirstOrDefault(),
                
            }).ToList();*/
            var catalog = _mapper.ProjectTo<CatalogModel>(_catalogRepo.Fetch(includeProperties: "CatalogBuyingDetails,CatalogSellingDetails"));
            return Ok(new PaginatedResponse<object>()
            {
                Data = new PaginateItems<object>()
                {
                    Items = catalog,
                    Total = _catalogRepo.Fetch().Select(x => x.Id).Count()
                },
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCatalogInputModel item)
        {
            await _catalogRepo.Insert(new Catalogs()
            {
                Name = item.Name,
                ItemNumber = item.ItemNumber,
                IBuyThisItem = item.IBuyThisItem,
                ISellThisItem = item.ISellThisItem,
                IInventoryThisItem = item.IInventoryThisItem,
                SalesAcount = item.SalesAccount,
                IncomeAcount = item.IncomeAccount,
                InventoryAccount = item.InventoryAccount,
                ManufacturerNo = item.ManufacturerNo,
                Description = item.Description
            });
            return Ok(new PaginatedResponse<object>()
            {
                Data = new PaginateItems<object>()
                {
                    Items = _catalogRepo.Fetch().ToList(),
                    Total = _catalogRepo.Fetch().Select(x => x.Id).Count()
                },
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
