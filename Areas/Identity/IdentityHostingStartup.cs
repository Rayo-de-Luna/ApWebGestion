using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AWGestion.Areas.Identity.Data;
using AWGestion.Data;

[assembly: HostingStartup(typeof(AWGestion.Areas.Identity.IdentityHostingStartup))]
namespace AWGestion.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AWGestionAuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AWGestionAuthDbContextConnection")));

                services.AddDefaultIdentity<AWGestionUsers>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AWGestionAuthDbContext>();
            });
        }
    }
}