using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Web.Application.Interfaces;

namespace OpenCMS.Web.Application.Services
{
    public class OpenCMSHttpClient: IOpenCMSHttpClient
    {
        private readonly IUserService _userService;
        private readonly IHttpClientFactory _clientFactory;

        public OpenCMSHttpClient(IUserService userService, IHttpClientFactory clientFactory)
        {
            _userService = userService;
            _clientFactory = clientFactory;
        }
        public HttpClient Create()
        {
            var http = _clientFactory.CreateClient("OpenCMS");
            http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _userService.TokenResponse.AccessToken);
            return http;
        }
    }
}
