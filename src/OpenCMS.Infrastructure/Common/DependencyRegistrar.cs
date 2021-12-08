using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenCMS.Application.EventHandlers;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Application.Repository;
using OpenCMS.Application.Services;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Infrastructure.Common
{
    public static class DependencyRegistrar
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryService<,>));
            services.AddHttpContextAccessor();
            services.AddScoped<ITenantService, TenantService>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddMediatR(typeof(UpdateLastCostHandler));
        }
    }
}
