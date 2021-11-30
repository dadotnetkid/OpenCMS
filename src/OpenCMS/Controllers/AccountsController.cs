using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;
using OpenCMS.Infrastructure.Common;
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Models.InputModels;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController, Guard]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<Accounts, string> _accountsRepo;
        private readonly IRepository<Classifications, string> _classificationRepo;
        private readonly IMapper _mapper;

        public AccountsController(IRepository<Accounts, string> accountsRepo, IRepository<Classifications, string> classificationRepo, IMapper mapper)
        {
            _accountsRepo = accountsRepo;
            _classificationRepo = classificationRepo;
            _mapper = mapper;
        }
        [HttpPost("{classificationId?}")]
        public IActionResult Post(string classificationId = "")
        {
            var classification = _classificationRepo.Find(classificationId);
            var accounts = _accountsRepo.Fetch();
            if (classification?.OrderBy != 0 && !string.IsNullOrEmpty(classificationId))
            {
                accounts = _accountsRepo.Fetch(x => x.ClassificationId == classificationId);
            }

            return Ok(new BaseResponse<object>()
            {
                Data = _mapper.ProjectTo<AccountModel>(accounts),
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpGet("GetAllClassification")]
        public IActionResult GetAllClassification()
        {
            return Ok(new BaseResponse<object>()
            {
                HttpStatusCode = System.Net.HttpStatusCode.OK,
                Data = _classificationRepo.Fetch().OrderBy(x => x.OrderBy)
            });
        }
    }
}
