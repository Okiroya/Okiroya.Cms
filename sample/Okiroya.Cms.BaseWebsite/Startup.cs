using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Okiroya.Cms.Mvc.Builder;
using Okiroya.Cms.Mvc.DependencyInjection;
using Microsoft.AspNetCore.Rewrite;

namespace Okiroya.Cms.BaseWebsite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddMvc();

            services.AddCms(
                (options) =>
                {
                options.SiteId = 1;

                    options.CmsDataOptions.ConnectionString = Configuration.GetConnectionString("cms");
                    options.CmsDataOptions.UseMsSql();

                    options.CmsAdministrationDataOptions.ConnectionString = "temp";
                    options.CmsAdministrationDataOptions.UseMsSqlEntityFramework(
                        (optionsBuilder) => 
                        {
                            optionsBuilder.EnableRetryOnFailure();
                        });
                });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddCmsLogger();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }

            app.UseStaticFiles();

            app.UseCms();

            app.UseMvc(app.UseCmsRoutes());

            app.UseRewriter(new RewriteOptions().AddCmsRewrite());
        }
    }
}
