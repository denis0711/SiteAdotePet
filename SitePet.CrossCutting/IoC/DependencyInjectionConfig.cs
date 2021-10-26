using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SitePet.Domain.Interfaces;
using SitePet.Infrastructure.Context;
using SitePet.Infrastructure.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitePet.CrossCutting.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection UseDependencieConfig
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MeuDbContext>
                (o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPetRepository, PetRepository>();
         

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
             .AddEntityFrameworkStores<MeuDbContext>();

            return services;
        }
    }
}
