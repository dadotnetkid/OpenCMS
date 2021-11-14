using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IUserService _userService;

        public AccountsService(IHttpClientFactory clientFactory, IUserService userService)
        {
            _clientFactory = clientFactory;
            _userService = userService;
        }
        public async Task<List<AccountsModel>> GetAll()
        {
            var http = _clientFactory.CreateClient("OpenCMS");

            http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _userService.TokenResponse.AccessToken);
            var result = await http.GetFromJsonAsync<BaseResponse<List<AccountsModel>>>("accounts");
            return result.Data;
        }
    }
}
