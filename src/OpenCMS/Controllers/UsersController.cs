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
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Models.InputModels;
using OpenCMS.Shared.Models.Models;

namespace OpenCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<Users, string> _userRepo;
        private readonly IRepository<Permissions, int> _permissionRepo;
        private readonly IRepository<PermissionsInRoles, int> _permissionInRoleRepo;
        private readonly IRepository<Roles, string> _rolesRepository;
        private readonly IUserService _userService;

        public UsersController(IRepository<Users, string> userRepo,
            IRepository<Permissions, int> permissionRepo,
            IRepository<PermissionsInRoles, int> permissionInRoleRepo,
            IRepository<Roles, string> rolesRepository, IUserService userService)
        {
            _userRepo = userRepo;
            _permissionRepo = permissionRepo;
            _permissionInRoleRepo = permissionInRoleRepo;
            _rolesRepository = rolesRepository;
            _userService = userService;
        }
        [HttpGet()]
        public IActionResult GetUsers()
        {
            var users = _userRepo.Fetch()
                .Select(x => new
                {
                    x.Id,
                    x.UserName,
                    x.Email,
                    x.FirstName,
                    x.MiddleName,
                    x.LastName,
                    userRoles = x.Roles.Select(r => new { r.Id, r.Role })
                });
            return Ok(new PaginatedBaseResponse<object>()
            {

                Data = new PaginatedBaseItems<object>()
                {
                    Items = users.ToList(),
                    Total = users.Select(x => x.Id).Count()
                },
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });
        }
        [HttpGet("get-roles")]
        public IActionResult GetRoles()
        {
            var result = _rolesRepository.GetAll();

            return Ok(new BaseResponse<object>()
            {

                Data = result.Select(x => new { x.Id, x.Role }),
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

                }, item.Password, item.UserRoles);
                return Ok(new BaseResponse<object>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                });
            }
            catch (Exception e)
            {
                return Ok(new ErrorResponse(e.Message));
            }

        }
        [HttpPatch()]
        public async Task<IActionResult> Update([FromBody] UserModel item)
        {
            try
            {
                var user = await _userService.Update(new Users()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    CreatedAt = DateTime.Now,
                    Email = item.Email,
                    UserName = item.UserName

                }, item.Password, item.UserRoles);
                return Ok(new BaseResponse<object>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                });
            }
            catch (Exception e)
            {
                return Ok(new ErrorResponse(e.Message));
            }

        }
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] RoleModel item)
        {
            try
            {
                var role = await _userService.CreateRole(new Roles()
                {
                    Role = item.Role
                });
                return Ok(new BaseResponse<object>()
                {
                    Data = _rolesRepository.GetAll(),
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                });
            }
            catch (Exception e)
            {
                return Ok(new ErrorResponse(e.Message));
            }
        }
        [HttpGet("get-all-permission")]
        public async Task<IActionResult> GetAllPermission(string roleId)
        {
            var permission = _permissionInRoleRepo.GetAll(x => x.RoleId == roleId, includeProperties: "Permission");

            try
            {
                return Ok(new BaseResponse<object>()
                {
                    Data = permission.Select(x => new
                    {
                        x.Id,
                        x.PermissionId,
                        x.Permission?.Permission,
                        x.RoleId,
                        x.CanAdd,
                        x.CanUpdate,
                        x.CanDelete
                    }),
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
