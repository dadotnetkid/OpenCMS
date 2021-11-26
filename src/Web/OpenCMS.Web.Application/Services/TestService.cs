using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OpenCMS.Web.Application.Interfaces;

namespace OpenCMS.Web.Application.Services
{
    public class TestService : ITestService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NavigationManager _navigationManager;

        public TestService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
        {
            _httpClientFactory = httpClientFactory;
            _navigationManager = navigationManager;
        }
        public void Get()
        {
            var http = _httpClientFactory.CreateClient("OpenCMS");

        }
    }
}
