using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Mvc.Internal;
using System;

namespace Okiroya.Cms.Mvc.Builder
{
    public static class CmsApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCms(this IApplicationBuilder app)
        {
            Guard.ArgumentNotNull(app);

            return app.UseMiddleware<ABGroupMiddleware>();
        }

        public static Action<IRouteBuilder> UseCmsRoutes(this IApplicationBuilder app)
        {
            return routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{*path}",
                    defaults: new { controller = "Cms", action = "Index" });
            };
        }
    }
}
