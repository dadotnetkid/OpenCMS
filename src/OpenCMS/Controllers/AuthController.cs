using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;
using OpenCMS.Domain.Models;
using OpenCMS.Infrastructure.Common;

namespace OpenCMS.Controllers
{

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<LoginModel> _mockCredentials;

        private JwtSettings _jwtSettings;

        public AuthController(IOptions<JwtSettings> jwtSettings,
            IAuthenticationService authenticationService,
            IUserService userService,
            IWebHostEnvironment env,
            IOptions<LoginModel> mockCredentials
            )
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _env = env;
            _mockCredentials = mockCredentials;

            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost(".auth")]
        public IActionResult Authenticate([FromBody] LoginModel item)
        {
            if (_env.IsDevelopment())
            {
                item.UserName = _mockCredentials.Value.UserName;
                item.Password = _mockCredentials.Value.Password;
            }
            var res = _authenticationService.Verify(item.UserName, item.Password);
            if (res.Succeeded)
            {
                var token = _authenticationService.GenerateToken(item.UserName);
                return Ok(new BaseResponse<object>()
                {
                    Data = token,
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                });
            }

            return Ok(new ErrorResponse()
            {
                HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                ErrorMessage = res.Errors?.FirstOrDefault().Description
            });
        }

        [HttpGet(".create")]
        public async Task<IActionResult> Create(CreateUserModel item)
        {
            await _userService.Create(new Users()
            {
                Email = item.UserName,
                UserName = item.UserName,
                FirstName = item.FirstName,
                MiddleName = item.MiddleName,
                LastName = item.LastName
            }, item.Password);
            return Ok();
        }

    }
}
