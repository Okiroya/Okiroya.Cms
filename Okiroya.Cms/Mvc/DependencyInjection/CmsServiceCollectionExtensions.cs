using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Okiroya.Campione.DataAccess;
using Okiroya.Campione.SystemUtility;
using Okiroya.Campione.SystemUtility.DI;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Mvc.Internal.FileProvider;
using Okiroya.Cms.SystemUtility;
using System;
using System.Linq;

namespace Okiroya.Cms.Mvc.DependencyInjection
{
    public static class CmsServiceCollectionExtensions
    {
        public static IServiceCollection AddCms(this IServiceCollection services, Action<CmsServiceOptions> configureOptions)
        {
            Guard.ArgumentNotNull(services);
            Guard.ArgumentNotNull(configureOptions);

            var environment = services.GetServiceFromCollection<IHostingEnvironment>();

            var options = new CmsServiceOptions();

            configureOptions(options);

            services.Configure<RazorViewEngineOptions>(
                p =>
                {
                    var existingProvider = p.FileProviders.Count > 0 ?
                        p.FileProviders[0] :
                        environment?.ContentRootFileProvider;

                    p.FileProviders.Clear();
                    p.FileProviders.Add(new CmsRazorPageFileProvider(existingProvider));
                }
            );

            services.AddSingleton(
                new CmsSiteSettings
                {
                    IsDebug = string.Equals(environment?.EnvironmentName?.Trim(), "Development", StringComparison.OrdinalIgnoreCase),
                    SiteId = options.SiteId
                });

            RegisterDependencyContainer<IGroupRandomDistribution>.Register(new GroupSimpleDistibution());

            RegisterDependencyContainer<IDataService>.SetDefault(CmsServiceOptions.CmsScope, options.CmsDataOptions.DataService);
            RegisterDependencyContainer<IDataService>.SetDefault(CmsServiceOptions.CmsAdministrationScope, options.CmsAdministrationDataOptions.DataService);

            CmsCommonCommandNames.RegisterCommandsScope(CmsServiceOptions.CmsScope, CmsServiceOptions.CmsAdministrationScope);

            return services;
        }

        public static T GetServiceFromCollection<T>(this IServiceCollection services)
        {
            return (T)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }
    }
}
