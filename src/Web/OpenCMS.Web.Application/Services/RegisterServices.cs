using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OpenCMS.Web.Application.Interfaces;

namespace OpenCMS.Web.Application.Services
{
    public static class Register
    {
        public static void RegisterServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAgentsService, AgentsService>();
            builder.Services.AddScoped<IPdfViewerService, PdfViewerService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ITestService, TestService>();
            builder.Services.AddScoped<ISfDataManagerService, SfDataManagerService>();
            builder.Services.AddScoped<ICardFilesService, CardFilesService>();
            builder.Services.AddScoped<ICatalogsService, CatalogsService>();
        }
        public static void RegisterServices(this IServiceCollection builder)
        {
            builder.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.AddScoped<IUserService, UserService>();
            builder.AddScoped<IAgentsService, AgentsService>();
            builder.AddScoped<IPdfViewerService, PdfViewerService>();
            builder.AddScoped<ITransactionService, TransactionService>();
            builder.AddScoped<ITestService, TestService>();
            builder.AddScoped<ISfDataManagerService, SfDataManagerService>();
            builder.AddScoped<ICardFilesService, CardFilesService>();
            builder.AddScoped<ICatalogsService, CatalogsService>();
        }
    }
}
