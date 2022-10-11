using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WFM_WebAPI.Helpers;
using WFM_WebAPI.Models;
using WFM_WebAPI.Repo;
using WFM_WebAPI.Services;

namespace WFM_WebAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("*");
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                  });
            });

            services.AddControllers();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IUserService, UserService>();
            services.TryAddScoped<IEmployeesService, EmployeesService>();
            services.TryAddScoped<ISkillsService, SkillsService>();
            var ConnectionString = Configuration.GetConnectionString("MySQLConnection");
            services.AddDbContext<SQL_DBContext>(options => options.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 11))));
            services.AddSwaggerGen(c =>
            { 
               c.SwaggerDoc("v1", new OpenApiInfo { Title = "WFM_Project", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WFM V1"));
            }

          

            app.UseHttpsRedirection();

            app.UseRouting();
           

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseCors(MyAllowSpecificOrigins);



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();

              
            });
  
        }
    }
}
