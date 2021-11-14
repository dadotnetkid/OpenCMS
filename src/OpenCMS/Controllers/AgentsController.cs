using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;
using OpenCMS.Infrastructure.Common;

namespace OpenCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AgentsController : ControllerBase
    {
        private readonly IRepository<Agents, int> _repository;

        public AgentsController(IRepository<Agents, int> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(
                    new PaginatedResponse<object>()
                    {
                        Data = new PaginateItems<object>
                        {
                            Items = _repository.GetAll(),
                            Total = _repository.GetAll().Count()
                        },
                        HttpStatusCode = System.Net.HttpStatusCode.OK
                    });
            }
            catch (Exception e)
            {
                return Ok(new ErrorResponse(e.Message));
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(
                    new BaseResponse<object>()
                    {
                        Data = _repository.Find(id),
                        HttpStatusCode = System.Net.HttpStatusCode.OK
                    });
            }
            catch (Exception e)
            {
                return Ok(new ErrorResponse(e.Message));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Agents item)
        {
            try
            {
                await _repository.Insert(item);
                return Ok(
                    new BaseResponse<object>()
                    {
                        Data = item,
                        HttpStatusCode = System.Net.HttpStatusCode.OK
                    });
            }
            catch (Exception e)
            {
                return Ok(new ErrorResponse(e.Message));
            }
        }
        [HttpPatch]
        public async Task<IActionResult> Update(Agents item)
        {
            try
            {
                await _repository.Update(item);
                return Ok(
                    new BaseResponse<object>()
                    {
                        Data = item,
                        HttpStatusCode = System.Net.HttpStatusCode.OK
                    });
            }
            catch (Exception e)
            {
                return Ok(new ErrorResponse(e.Message));
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.Delete(id);
                return Ok(
                    new BaseResponse<object>()
                    {
                        Data = _repository.GetAll(),
                        HttpStatusCode = System.Net.HttpStatusCode.OK
                    });
            }
            catch (Exception e)
            {
                return Ok(new ErrorResponse(e.Message));
            }
        }
    }
}
