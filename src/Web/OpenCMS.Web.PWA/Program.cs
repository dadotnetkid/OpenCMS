using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OpenCMS.Shared.Models;
using OpenCMS.Shared.Validators;
using OpenCMS.Web.Application.Interfaces;
using OpenCMS.Web.Application.Services;
using Syncfusion.Blazor;
using MudBlazor.Services;
using OpenCMS.Web.Application.ApiClients;

namespace OpenCMS.Web.PWA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM2NzQ4QDMxMzkyZTMzMmUzMGdhUGVUOFFtWngraWt5alkrdlExM1N6alFYMFpIUWIyVlZlajVYSUhrU2M9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RegisterServices();
            builder.Services.AddTransient<IValidator<TransactionItemModel>, SalesItemsValidators>();
            builder.Services.AddTransient<IValidator<CardFilesModel>, CardFilesValidator>();
            builder.Services.AddTransient<IValidator<PaymentsModel>, TransactionMakePaymentValidator>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
     
            builder.Services.AddHttpClient("OpenCMS", c =>
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
            builder.Services.AddScoped<IOpenCMSHttpClient, OpenCMSHttpClient>();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddMudServices();
            builder.RegisterRefit();
            var host = builder.Build();
            var userService = host.Services.GetRequiredService<IUserService>();


            await userService.Initialize();

            await host.RunAsync();

        }
    }
}
