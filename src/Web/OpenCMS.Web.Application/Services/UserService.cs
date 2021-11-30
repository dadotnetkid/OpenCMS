using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenCMS.Domain.Models;
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Models.Models;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IHttpClientFactory _httpClientFactory;
        public TokenResponse TokenResponse { get; set; }
        private List<Claim> Claims { get; set; }
        public List<string> UserRoles { get; set; }

        public UserService(ILocalStorageService localStorageService, IHttpClientFactory httpClientFactory)
        {
            _localStorageService = localStorageService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaginatedBaseItems<List<UserModel>>> GetAll()
        {
            var http = _httpClientFactory.CreateClient("OpenCMS");
            var token = await GetTokenResponse();
            http.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.AccessToken);
            var result = await http.GetFromJsonAsync<PaginatedBaseResponse<List<UserModel>>>("users");
            return result.Data;
        }

        public string GetUserId()
        {

            return Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }
        public List<string> GetUserRoles()
        {
            return Claims.Where(x => x.Type == "role").Select(x => x.Value).ToList();
        }

        public async Task<List<RoleModel>> GetRoles()
        {
            var http = _httpClientFactory.CreateClient("OpenCMS");
            var get = await http.GetFromJsonAsync<BaseResponse<List<RoleModel>>>("users/get-roles");
            return get?.Data;
        }

        public bool GetUserIsRoles(params string[] roles)
        {
            var _userRoles = GetUserRoles();
            return _userRoles.Any(x => roles.Contains(x));

        }

        private async Task<IEnumerable<Claim>> JwtClaims()
        {
            var token = await _localStorageService.GetItem<TokenResponse>("token");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token.AccessToken);
            return jwtSecurityToken.Claims;
        }

        public async Task Initialize()
        {
            var token = await _localStorageService.GetItem<TokenResponse>("Token");

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token?.AccessToken);
                this.Claims = jwtSecurityToken.Claims.ToList();

                TokenResponse = token;
            }
        }
        public async Task<TokenResponse> GetTokenResponse()
        {
            var token = await _localStorageService.GetItem<TokenResponse>("Token");

            return token;
        }
        public bool IsAuthenticated()
        {
            if (TokenResponse == null)
                return false;
            else if (TokenResponse.Expire <= System.DateTime.UtcNow)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<BaseResponse> CreateUser(UserModel item)
        {
            var http = _httpClientFactory.CreateClient("OpenCMS");
            var token = await this.GetTokenResponse();
            http.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.AccessToken);
            var post = await http.PostAsJsonAsync("users", item);
            if (!post.IsSuccessStatusCode)
                return new ErrorResponse(post.ReasonPhrase);
            return new BaseResponse() { HttpStatusCode = HttpStatusCode.OK };
        }



        public string GetUserName()
        {
            return TokenResponse.UserName;
        }
        public string GetGivenName()
        {
            return this.Claims.Find(x => x.Type =="given_name")?.Value;
        }

        public async Task<BaseResponse> UpdateUser(UserModel item)
        {
            var http = _httpClientFactory.CreateClient("OpenCMS");
            var token = await GetTokenResponse();
            http.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.AccessToken);
            var patch = await http.PatchAsync("users",
                new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json"));
            if (!patch.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<ErrorResponse>(await patch.Content.ReadAsStringAsync());

            return new BaseResponse<object>()
            {
                HttpStatusCode = HttpStatusCode.OK
            };
        }
        public async Task<BaseResponse> CreateRole(RoleModel item)
        {
            var http = _httpClientFactory.CreateClient("OpenCMS");
            var token = await GetTokenResponse();
            http.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.AccessToken);
            var post = await http.PostAsync("users/create-role", new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json"));
            if (!post.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<ErrorResponse>(await post.Content.ReadAsStringAsync());
            return new BaseResponse() { HttpStatusCode = HttpStatusCode.OK };
        }

        public async Task<BaseResponse> GetPermissionsInRoles(string roleId)
        {
            var http = _httpClientFactory.CreateClient("OpenCMS");
            var token = await GetTokenResponse();
            http.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.AccessToken);
            var post = await http.GetAsync("users/get-all-permission?roleId=" + roleId);
            if (!post.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<ErrorResponse>(await post.Content.ReadAsStringAsync());
            return JsonSerializer.Deserialize<BaseResponse<List<PermissionsInRolesModel>>>(
                await post.Content.ReadAsStringAsync());
        }
    }
}

