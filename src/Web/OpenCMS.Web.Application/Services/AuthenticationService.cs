using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OpenCMS.Domain.Models;
using OpenCMS.Shared.Models;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        private readonly IUserService _userService;
        private readonly IHttpClientFactory _clientFactory;

        public AuthenticationService(NavigationManager navigationManager, ILocalStorageService localStorageService, IUserService userService, IHttpClientFactory clientFactory)
        {
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _userService = userService;
            _clientFactory = clientFactory;
        }
        public async Task<LoginResult> Login(string userName, string password)
        {

            var http = _clientFactory.CreateClient("OpenCMS");
            var post = await http.PostAsync(".auth", new StringContent(JsonSerializer.Serialize(new { userName, password }, typeof(object)), Encoding.UTF8, "application/json"));
          string content = await post.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BaseResponse<TokenResponse>>(content);
            await _localStorageService.SetItem<TokenResponse>("Token", result.Data);
            await _userService.Initialize();
            return new LoginResult()
            {
                IsSuccess = true
            };
        }



        public async Task Logout()
        {
            await _localStorageService.RemoveItem("Token");
            _userService.TokenResponse = null;
            _navigationManager.NavigateTo("auth/login");
        }
    }
}
