using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;
using OpenCMS.Domain.Models;

namespace OpenCMS.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IRepository<Users, string> _userRepo;
        private readonly IRepository<Roles, string> _rolesRepo;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private JwtSettings _jwtSettings;

        public AuthenticationService(IUserService userService, IRepository<Users, string> userRepo,
            IRepository<Roles,string> rolesRepo,
            IOptions<JwtSettings> jwtSettings,
            IPasswordHasher<Users> passwordHasher)
        {
            _userService = userService;
            _userRepo = userRepo;
            _rolesRepo = rolesRepo;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
        }

        public IdentityResult Verify(string userName, string password)
        {
            var user = _userRepo.Find(x => x.UserName == userName);
            if (user == null)
                return IdentityResult.Failed(new IdentityError() { Code = "001", Description = "No Username Found" });

            var res = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (res == PasswordVerificationResult.Success)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(
                new IdentityError()
                {
                    Code = "001",
                    Description = "Invalid UserName and Password"
                });
        }

        public object GenerateToken(string userName)
        {
            var user = _userRepo.Find(x => x.UserName == userName,"Roles");
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expire = DateTime.UtcNow.AddHours(6);
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName) ,
                new(ClaimTypes.NameIdentifier, user.Id) ,
                new (ClaimTypes.Email,user.Email)
            };
            if (user.Roles.Any())
            {
                foreach (var i in user.Roles)
                {
                    claims.Add(new(ClaimTypes.Role, i.Role));
                }
            }
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return new
            {
                userName=userName,
                access_token=jwtToken,
                expire=expire,

            };
        }
    }
}