using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OpenCMS.ApiClient.Interfaces;
using OpenCMS.Web.Application.Interfaces;
using Refit;
using IAccountsService = OpenCMS.ApiClient.Interfaces.IAccountsService;

namespace OpenCMS.ApiClient
{
    public static class RegisterApiClient
    {
        public static void RegisterRefit(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient<AuthHeaderHandler>();
            builder.Services
                .AddRefitClient<IAccountsService>()
                .HttpBuilder(builder);
            builder.Services
                .AddRefitClient<ICardFilesService>()
                .HttpBuilder(builder)
                .AddHttpMessageHandler<AuthHeaderHandler>();
            builder.Services
                .AddRefitClient<ICatalogsService>()
                .HttpBuilder(builder)
                .AddHttpMessageHandler<AuthHeaderHandler>();
        }

        private static IHttpClientBuilder HttpBuilder(this IHttpClientBuilder httpClientBuilder, WebAssemblyHostBuilder builder)
        {
            return httpClientBuilder.ConfigureHttpClient(c =>
            {
                var config = builder.Configuration;
                var url = config.GetSection("OpenCMS:ApiUrl");
                var key = config.GetSection("OpenCMS:Key");
                var secret = config.GetSection("OpenCMS:Secret");
                var apiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(key.Value + ":" + secret.Value));
                c.DefaultRequestHeaders.Add("ApiKey", apiKey);
                c.DefaultRequestHeaders.Add("Domain", config.GetSection("OpenCMS:Domain").Value);
                c.BaseAddress = new Uri(url.Value);
            });
        }

        
    }
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IUserService _userService;

        public AuthHeaderHandler(IUserService userService)
        {
            _userService = userService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _userService.GetTokenResponse();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
