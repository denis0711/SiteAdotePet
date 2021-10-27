using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SitePet.Infrastructure.Context;
using SitePet.Mvc.Extensions;

namespace SitePet.Mvc.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.LoginPath = "/Login";
                  options.AccessDeniedPath = "/acesso-negado";
              });


            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IndentityMensagensPortugues>()
                .AddEntityFrameworkStores<MeuDbContext>();

            return services;
        }
    }
}
