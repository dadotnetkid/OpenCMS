using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Application.Services;
using Syncfusion.Blazor;

namespace OpenCMS.Web.PWA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM2NzQ4QDMxMzkyZTMzMmUzMGdhUGVUOFFtWngraWt5alkrdlExM1N6alFYMFpIUWIyVlZlajVYSUhrU2M9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAgentsService, AgentsService>();
            builder.Services.AddScoped<ICardFilesService, CardFilesService>();
            builder.Services.AddScoped<IAccountsService, AccountsService>();
            builder.Services.AddScoped<ICatalogsService, CatalogsService>();
            builder.Services.AddScoped<IPdfViewerService, PdfViewerService>();
          

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient("OpenCMS", c =>
            {

                var url = builder.Configuration.GetSection("OpenCMS:ApiUrl");
                c.BaseAddress = new Uri(url.Value);
            });
            builder.Services.AddScoped<IOpenCMSHttpClient, OpenCMSHttpClient>();
            builder.Services.AddSyncfusionBlazor();

            var host = builder.Build();
            var userService = host.Services.GetRequiredService<IUserService>();


            await userService.Initialize();

            await host.RunAsync();

        }
    }
}
