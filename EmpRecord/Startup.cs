using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmpRecord.Models;
using EmpRecord.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace EmpRecord
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Used to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmployeeHearderConfiguration>(this.Configuration.GetSection("EmployeeSettings:Header"));
            services.AddDbContext<EmployeeContext>(opt => opt.UseInMemoryDatabase("EmployeeList"));
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            services.AddTransient<IEmployeeRepo, EmployeeRepo>();
            services.AddControllers(options =>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention());
            });
            services.AddSwaggerGen(config =>
            {
                var titleBase = "Employee Record Application";
                var description = "This is a web API for maintaining Employee records";
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = titleBase + " v1",
                    Description = description,
                });
                config.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = titleBase + " v2",
                    Description = description,
                });
            });
        }

        // Used to configure the HTTP request pipeline. (Middleware is implemented here)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
            });
            //app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.Run(context => { throw new Exception("error"); });
        }
    }
}
