using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.SystemUtility;
using System;

namespace Okiroya.Cms.API
{
    public static class App
    {
        public static AppSite GetCurrentSite(ICmsContext context)
        {
            Guard.ArgumentNotNull(context);

            return new AppSite(context);
        }

        public static SiteAccessor WorkWith(this AppSite site)
        {
            return new SiteAccessor(site);
        }

        public static MenuAccessor Menu(this SiteAccessor site)
        {
            Guard.ArgumentNotNull(site);

            return new MenuAccessor(site.Context);
        }

        public static PageAccessor Pages(this SiteAccessor site)
        {
            Guard.ArgumentNotNull(site);

            return new PageAccessor(site.Context);
        }

        public static ContentAccessor Content(this SiteAccessor site)
        {
            Guard.ArgumentNotNull(site);

            return new ContentAccessor(site.Context);
        }
    }
}
