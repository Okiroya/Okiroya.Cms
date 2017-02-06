using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.SystemUtility;
using System;

namespace Okiroya.Cms.API
{
    public class SiteAccessor : IHideObjectMembersForFluentApi
    {
        internal ICmsContext Context { get; private set; }

        public SiteAccessor(AppSite site)
        {
            Guard.ArgumentNotNull(site);

            Context = site.Context;
        }
    }
}
