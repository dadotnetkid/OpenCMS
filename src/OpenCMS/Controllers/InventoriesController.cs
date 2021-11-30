using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;
using OpenCMS.Infrastructure.Common;
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Models.InputModels;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IRepository<Inventories, int> _inventoriesRepo;

        public InventoriesController(IRepository<Inventories, int> inventoriesRepo)
        {
            _inventoriesRepo = inventoriesRepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new PaginatedBaseResponse<object>()
            {
                Data = new PaginatedBaseItems<object>()
                {
                    Items = _inventoriesRepo.Fetch().ToList(),
                    Total = _inventoriesRepo.Fetch().Select(x => x.Id).Count()
                },
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });

        }
    }
}
