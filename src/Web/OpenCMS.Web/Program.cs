using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MudBlazor.Services;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Application.Services;

namespace OpenCMS.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAgentsService, AgentsService>();
            builder.Services.AddScoped<ICardFilesService, CardFilesService>();
            builder.Services.AddScoped<IAccountsService, AccountsService>();
            builder.Services.AddScoped<ICatalogsService, CatalogsService>();
            builder.Services.AddScoped<IPdfViewerService, PdfViewerService>();
            builder.Services.AddScoped<IOpenCMSHttpClient, OpenCMSHttpClient>();
            builder.Services.AddMudServices();

            builder.Services.AddHttpClient("OpenCMS", c =>
            {
                
                var url = builder.Configuration.GetSection("OpenCMS:ApiUrl");
                c.BaseAddress = new Uri(url.Value);
            });

            var host = builder.Build();
            var userService = host.Services.GetRequiredService<IUserService>();


            await userService.Initialize();

            await host.RunAsync();
        }
    }
}
