using Microsoft.AspNetCore.Http;
using Okiroya.Campione.SystemUtility;
using Okiroya.Campione.SystemUtility.DI;
using Okiroya.Cms.SystemUtility;
using System;
using System.Threading.Tasks;

namespace Okiroya.Cms.Mvc.Internal
{
    public class ABGroupMiddleware
    {
        private readonly RequestDelegate _next;

        public ABGroupMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Guard.ArgumentNotNull(context);

            if (context.Request.Cookies != null)
            {
                string abGroupValue;
                byte abGroup = 0;
                if (!(context.Request.Cookies.TryGetValue(DefaultCmsContext.CmsContextUserABGroupKey, out abGroupValue) &&
                    byte.TryParse(abGroupValue, out abGroup)))
                {
                    abGroup = RegisterDependencyContainer<IGroupRandomDistribution>.Resolve().GetNextGroup();

                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Path = "/",
                        Expires = DateTime.Now.AddYears(1)
                    };

                    if (context.Request.Host.HasValue && !string.Equals(context.Request.Host.Host, "localhost"))
                    {
                        cookieOptions.Domain = $".{context.Request.Host.ToUriComponent()}";
                    }

                    context.Response.Cookies.Append(
                        key: DefaultCmsContext.CmsContextUserABGroupKey,
                        value: abGroup.ToString(),
                        options: cookieOptions);
                }

                context.Items[DefaultCmsContext.CmsContextUserABGroupKey] = abGroup;
            }

            await _next.Invoke(context);
        }
    }
}
