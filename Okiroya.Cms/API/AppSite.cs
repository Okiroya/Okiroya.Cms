using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.SystemUtility;
using System;

namespace Okiroya.Cms.API
{
    public class AppSite : IHideObjectMembersForFluentApi
    {
        internal ICmsContext Context { get; private set; }

        public int SiteId => Context.SiteId;

        public AppSite(ICmsContext context)
        {
            Guard.ArgumentNotNull(context);

            Context = context;
        }
    }
}
