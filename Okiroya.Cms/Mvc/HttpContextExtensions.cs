using Microsoft.AspNetCore.Http;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Mvc.Internal;
using Okiroya.Cms.SystemUtility;
using System;
using System.Linq;

namespace Okiroya.Cms.Mvc
{
    public static class HttpContextExtensions
    {
        public static byte GetCurrentABGroup(this HttpContext context)
        {
            Guard.ArgumentNotNull(context);

            return context.Items.ContainsKey(DefaultCmsContext.CmsContextUserABGroupKey) ?
                (byte)context.Items[DefaultCmsContext.CmsContextUserABGroupKey] :
                byte.MinValue;
        }

        public static ICmsContext GetCmsContext(this HttpContext context)
        {
            Guard.ArgumentNotNull(context);

            return new DefaultCmsContext(context.RequestServices, context.Items.ToDictionary(p => p.Key.ToString(), p => p.Value));
        }
    }
}
