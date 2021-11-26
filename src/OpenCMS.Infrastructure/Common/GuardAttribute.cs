using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Infrastructure.Common
{
    public class GuardAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public GuardAttribute()
        {

        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            if (ValidateEmptyApiKey(context)) return;
            if (ValidateIsValidBase64String(context)) return;
            if (ValidateDomainExist(context)) return;


            if (ValidateApiKeyContent(context)) return;
            if (ValidateIsApiKeyExists(context)) return;
            return;
        }

        private bool ValidateDomainExist(AuthorizationFilterContext context)
        {
            var domain = context.HttpContext.Request.Headers["Domain"];
            if (string.IsNullOrEmpty(domain))
            {
                context.Result = new GuardUnAuthorizationResult(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ErrorMessage = "Access Denied"
                });
                return true;
            }
            return false;
        }

        private bool ValidateIsApiKeyExists(AuthorizationFilterContext context)
        {
            var db = context.HttpContext.RequestServices.GetRequiredService<OpenCMSDb>();
            var _apiKey = context.HttpContext.Request.Headers["ApiKey"].ToString();
            var apiKey = Encoding.UTF8.GetString(Convert.FromBase64String(_apiKey)).Split(":").ToList();
            var domain = context.HttpContext.Request.Headers["Domain"].ToString();
            var key = apiKey[0];
            var secret = apiKey[1];
            var result = db.Tenants.Any(x => x.ApiKey == key && x.ApiSecret == secret && x.Domain == domain);
            if (!result)
            {
                context.Result = new GuardUnAuthorizationResult(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ErrorMessage = "Access Denied"
                });
                return true;
            }

            return false;
        }

        private bool ValidateApiKeyContent(AuthorizationFilterContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["ApiKey"];
            var _apiKey = Encoding.UTF8.GetString(Convert.FromBase64String(apiKey)).Split(':').ToList();
            if (_apiKey.Count < 2)
            {
                context.Result = new GuardUnAuthorizationResult(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ErrorMessage = "Access Denied"
                });
                return true;
            }

            return false;
        }

        private bool ValidateEmptyApiKey(AuthorizationFilterContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                context.Result = new GuardUnAuthorizationResult(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ErrorMessage = "Access Denied"
                });
                return true;
            }

            return false;
        }
        private bool ValidateIsValidBase64String(AuthorizationFilterContext context)
        {
            try
            {
                var apiKey = context.HttpContext.Request.Headers["ApiKey"];
                Encoding.UTF8.GetString(Convert.FromBase64String(apiKey));
                return false;
            }
            catch (Exception e)
            {
                context.Result = new GuardUnAuthorizationResult(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ErrorMessage = "Access Denied"
                });
                return true;
            }
        }
    }

    public class GuardUnAuthorizationResult : JsonResult
    {
        public GuardUnAuthorizationResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}
