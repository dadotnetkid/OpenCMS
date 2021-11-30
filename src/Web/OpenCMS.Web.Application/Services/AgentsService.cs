using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Services
{
    public class AgentsService : IAgentsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserService _userService;
        private HttpClient _http;

        public AgentsService(IHttpClientFactory httpClientFactory,IUserService userService)
        {
            _httpClientFactory = httpClientFactory;
            _userService = userService;
            _http = _httpClientFactory.CreateClient("OpenCMS");
            _http.DefaultRequestHeaders.Add("Authorization","Bearer "+ _userService.TokenResponse.AccessToken);
        }
        public async Task<List<AgentsModel>> GetAll()
        {
     

            var get = await _http.GetAsync("api/agents");
            if (!get.IsSuccessStatusCode) return default;
            var content = await get.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<PaginatedBaseResponse<List<AgentsModel>>>(content);
            return obj.Data.Items;
        }
    }
}
