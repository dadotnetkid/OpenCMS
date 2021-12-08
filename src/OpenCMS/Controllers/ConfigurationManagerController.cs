using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;
using OpenCMS.Shared.Models;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigurationManagerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ConfigurationManagements, int> _configurationManagementRepo;

        public ConfigurationManagerController(IMapper mapper,
            IRepository<ConfigurationManagements, int> configurationManagementRepo
            )
        {
            _mapper = mapper;
            _configurationManagementRepo = configurationManagementRepo;
        }
        [HttpGet("configurationName")]
        public IActionResult GetConfigurationManagement(string configurationName)
        {
            var model = _mapper.Map<HashConfigurationModel>(_configurationManagementRepo.Find(x => x.ConfigurationName == configurationName));
            return Ok(new BaseResponse<object>()
            {
                Data = model,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
