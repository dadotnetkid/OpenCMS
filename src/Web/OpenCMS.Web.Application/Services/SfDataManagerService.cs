using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Application.Interfaces;

namespace OpenCMS.Web.Application.Services
{
    public class SfDataManagerService : ISfDataManagerService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public SfDataManagerService(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }
        public SfDataManagerModel Get()
        {
            var url = _configuration.GetSection("OpenCMS:ApiUrl");
            var key = _configuration.GetSection("OpenCMS:Key");
            var secret = _configuration.GetSection("OpenCMS:Secret");
            var apiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(key.Value + ":" + secret.Value));
            var headers = new Dictionary<string, string>();
            var token = _userService.TokenResponse;
            headers.Add("Authorization", $"Bearer {token.AccessToken}");
            headers.Add("ApiKey", apiKey);
            headers.Add("Domain", _configuration.GetSection("OpenCMS:Domain").Value);
            return new SfDataManagerModel()
            {
                HeaderData = headers,
                Url = url.Value
            };
        }
    }
}
