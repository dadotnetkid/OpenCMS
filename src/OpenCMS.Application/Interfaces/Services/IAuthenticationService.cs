using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public IdentityResult Verify(string userName, string password);
        public object GenerateToken(string userName);
    }
}
