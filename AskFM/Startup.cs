using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.Repositories;
using AskFM.Repositories.IRepositories;
using AskFM.Services;
using AskFM.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AskFM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<IFileStorageService, FileStorageService>();
            services.AddControllersWithViews();

            services.AddTransient<IImageService, ImageService>();
            services.AddControllersWithViews();

            services.AddScoped<IImageMetaDataRepository, ImageMetaDataRepository>();
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    // подключение аутентификации
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            bool exists = System.IO.Directory.Exists("C:\\Image");
            if (!exists)
                System.IO.Directory.CreateDirectory("C:\\Image");
        }
    }
}
