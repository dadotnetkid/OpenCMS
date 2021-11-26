using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly OpenCMSDb _db;

        public TenantService(IHttpContextAccessor httpContextAccessor, OpenCMSDb db)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }
        public int GetTenant()
        {
            var context = _httpContextAccessor;
            var _apiKey = context.HttpContext.Request.Headers["ApiKey"].ToString();
            var apiKey = Encoding.UTF8.GetString(Convert.FromBase64String(_apiKey)).Split(":").ToList();
            var domain = context.HttpContext.Request.Headers["Domain"].ToString();
            var key = apiKey[0];
            var secret = apiKey[1];
            var result = _db.Tenants.FirstOrDefault(x => x.ApiKey == key && x.ApiSecret == secret && x.Domain == domain);
            if (result != null)
                return result.Id;
            return 0;
        }
    }
}
