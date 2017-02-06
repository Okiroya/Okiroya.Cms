using Okiroya.Campione.Domain;
using Okiroya.Cms.ServiceFacade;
using System;

namespace Okiroya.Cms.Domain
{
    public class CmsTemplate : Int32EntityObject
    {
        private CmsTemplateRegion[] _templateRegions;

        public string Name { get; set; }

        public string TemplatePath { get; set; }

        public CmsTemplateRegion[] TemplateRegions
        {
            get
            {
                if (_templateRegions == null)
                {
                    _templateRegions = CmsTemplateService.GetTemplateRegions(Id);
                }

                return _templateRegions;
            }
            set
            {
                _templateRegions = value;
            }
        }

        public bool HasRegions => TemplateRegions != null;
    }
}
