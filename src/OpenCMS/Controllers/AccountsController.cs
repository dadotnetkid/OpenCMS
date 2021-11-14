using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;
using OpenCMS.Infrastructure.Common;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<Accounts, string> _accountsRepo;

        public AccountsController(IRepository<Accounts, string> accountsRepo)
        {
            _accountsRepo = accountsRepo;
        }
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok(new BaseResponse<List<Accounts>>()
            {
                Data = _accountsRepo.GetAll(),
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
