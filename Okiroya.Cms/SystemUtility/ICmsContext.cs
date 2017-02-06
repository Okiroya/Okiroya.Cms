using Okiroya.Cms.Domain;
using System;

namespace Okiroya.Cms.SystemUtility
{
    public interface ICmsContext
    {
        int SiteId { get; }

        byte CurrentABGroup { get; }

        bool IsDebugEnabled { get; }

        CmsPage CurrentPage { get; }
    }
}
