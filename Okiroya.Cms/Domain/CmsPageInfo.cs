using System;

namespace Okiroya.Cms.Domain
{
    public class CmsPageInfo : CmsABTestEntity<int>
    {
        public int CommonId { get; set; }

        public int SiteGroupId { get; set; }

        public int? SiteId { get; set; }

        public string Url { get; set; }

        public string TemplatePath { get; set; }

        public bool CanCached { get; set; }

        public CmsPageInfo()
            : base()
        {
            EntityTypeSysName = CmsPage.CmsPageEntityType;
        }
    }
}
