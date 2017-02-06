using Okiroya.Cms.ServiceFacade;
using System;

namespace Okiroya.Cms.Domain
{
    public class CmsPage : CmsEntity<int>
    {
        public const string CmsPageEntityType = "CmsPage";

        private CmsTemplate _template;

        public int ParentPageId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int TemplateId { get; set; }

        public bool IsVisible { get; set; }

        public bool CanCached { get; set; }

        public CmsTemplate Template
        {
            get
            {
                if (_template == null)
                {
                    _template = CmsTemplateService.GetTemplate(TemplateId);
                }

                return _template;
            }
            set
            {
                _template = value;
            }
        }

        public CmsPage() 
            : base()
        {
            EntityTypeSysName = CmsPageEntityType;
        }
    }
}
