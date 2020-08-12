using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.Repositories;
using AskFM.Repositories.IRepositories;
using AskFM.Services;
using AskFM.Services.Contracts;
using AutoMapper;
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


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<IFileStorageService, FileStorageService>();
            services.AddControllersWithViews();

            services.AddScoped<IImageMetaDataRepository, ImageMetaDataRepository>();

            services.AddTransient<IImageService, ImageService>();
            services.AddControllersWithViews();

            services.AddScoped<IQuestionRepository, QuestionRepository>();

            services.AddTransient<IQuestionService, QuestionService>();
            services.AddControllersWithViews();

            services.AddScoped<ICommentsRepositories, CommentsRepositories>();

            services.AddTransient<ICommentsService, CommentsService>();
            services.AddControllersWithViews();

            services.AddSwaggerGen();
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
