using System;

namespace Okiroya.Cms.Domain
{
    public class CmsMenuItem : CmsABTestEntity<int>
    {
        public int CommonPageId { get; set; }

        public int? ParentPageId { get; set; }

        public int HierarchyLevel { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public bool IsHttps { get; set; }
    }
}
