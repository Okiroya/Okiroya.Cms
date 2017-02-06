using System;

namespace Okiroya.Cms.Domain
{
    public class CmsPageControl : CmsABTestEntity<int>
    {
        public const string CmsPageControlEntityType = "CmsPageControl";

        public int PageControlId { get; set; } //TODO: хранимка не маппит Id, нужно разобраться

        public int TemplateRegionId { get; set; }

        public byte ControlTypeId { get; set; }

        public CmsPageControlTypes ControlType => (CmsPageControlTypes)ControlTypeId;

        public int ControlContentId { get; set; }

        public int ControlViewComponentId { get; set; }

        public int ControlModuleId { get; set; }

        public CmsPageControl() 
            : base()
        {
            EntityTypeSysName = CmsPageControlEntityType;
        }
    }
}
