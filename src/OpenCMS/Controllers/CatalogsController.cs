using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;
using OpenCMS.Domain.Models.InputModels;
using OpenCMS.Infrastructure.Common;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly IRepository<Catalogs, int> _catalogRepo;

        public CatalogsController(IRepository<Catalogs, int> catalogRepo)
        {
            _catalogRepo = catalogRepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
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
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCatalogInputModel item)
        {
            await _catalogRepo.Insert(new Catalogs()
            {
                Name=item.Name,
                ItemNumber=item.ItemNumber,
                IBuyThisItem=item.IBuyThisItem,
                ISellThisItem=item.ISellThisItem,
                IInventoryThisItem=item.IInventoryThisItem,
                SalesAcount=item.SalesAccount,
                IncomeAcount=item.IncomeAccount,
                InventoryAccount=item.InventoryAccount,
                ManufacturerNo=item.ManufacturerNo,
                Description=item.Description
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
