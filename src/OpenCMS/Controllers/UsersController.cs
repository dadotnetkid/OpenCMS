using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;
using OpenCMS.Domain.Models;
using OpenCMS.Infrastructure.Common;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<Users, string> _userRepo;
        private readonly IUserService _userService;

        public UsersController(IRepository<Users, string> userRepo, IUserService userService)
        {
            _userRepo = userRepo;
            _userService = userService;
        }
        [HttpGet()]
        public IActionResult GetUsers()
        {
            var users = _userRepo.Fetch()
                .Select(x => new { x.Id, x.UserName, x.Email, x.FirstName, x.MiddleName, x.LastName });
            return Ok(new PaginatedResponse<object>()
            {

                Data = new PaginateItems<object>()
                {
                    Items = users.ToList(),
                    Total = users.Select(x => x.Id).Count()
                },
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] UserModel item)
        {
            try
            {
                var user = await _userService.Create(new Users()
                {
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    CreatedAt = DateTime.Now,
                    Email = item.Email,
                    UserName = item.UserName

                }, item.Password);
                return Ok(new BaseResponse<object>
                {
                    Data = user,
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
