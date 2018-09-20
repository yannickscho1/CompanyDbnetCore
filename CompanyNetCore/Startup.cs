using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyNetCore.Helper;
using CompanyNetCore.Interface;
using CompanyNetCore.Model;
using CompanyNetCore.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TobitLogger.Core;
using TobitLogger.Logstash;
using TobitLogger.Middleware;
using TobitWebApiExtensions.Extensions;

namespace CompanyNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILogContextProvider, RequestGuidContextProvider>();

            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));
            services.Configure<ChaynsApiInfo>(Configuration.GetSection("ChaynsBackendApi"));
            services.AddSingleton<IDbContext, Helper.DbContext>();
            services.AddScoped<CompanyRepository, CompanyRepo>();
            services.AddScoped<IMessageHelper, MessageHelper>();

            services.AddChaynsToken();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ILogContextProvider logContextProvider)
        {
            loggerFactory.AddLogstashLogger(Configuration.GetSection("Logger"), logContextProvider: logContextProvider);
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler();

            app.UseRequestLogging();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
