using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.SystemUtility;
using System;
using System.Collections.Generic;
using Okiroya.Cms.Domain;
using Okiroya.Cms.Mvc.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Okiroya.Cms.Mvc.Internal
{
    public class DefaultCmsContext : ICmsContext
    {
        public const string CmsContextUserABGroupKey = "CmsContext.UserABGroup";
        public const string CmsContextCurrentPageKey = "CmsContext.CurrentPage";

        public bool IsDebugEnabled { get; protected set; }

        public int SiteId { get; protected set; }

        public byte CurrentABGroup { get; protected set; }

        public CmsPage CurrentPage { get; protected set; }

        public DefaultCmsContext(IServiceProvider requestServices, IDictionary<string, object> items)
        {
            Guard.ArgumentNotNull(requestServices);
            Guard.ArgumentNotNull(items);

            var cmsSiteSettings = requestServices.GetRequiredService<CmsSiteSettings>();

            IsDebugEnabled = cmsSiteSettings != null ?
                cmsSiteSettings.IsDebug :
                false;

            SiteId = cmsSiteSettings != null ?
                cmsSiteSettings.SiteId :
                0;

            CurrentABGroup = items.ContainsKey(CmsContextUserABGroupKey) ?
                (byte)items[CmsContextUserABGroupKey] :
                byte.MinValue;

            CurrentPage = items.ContainsKey(CmsContextCurrentPageKey) ?
                items[CmsContextCurrentPageKey] as CmsPage :
                null;
        }
    }
}
