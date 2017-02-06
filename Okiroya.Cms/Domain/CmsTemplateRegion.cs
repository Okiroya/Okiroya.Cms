using Okiroya.Campione.Domain;
using Okiroya.Cms.ServiceFacade;
using System;

namespace Okiroya.Cms.Domain
{
    public class CmsTemplateRegion : Int32EntityObject
    {
        private CmsPageControl[] _regionControls;

        public int TemplateId { get; set; }

        public string Name { get; set; }

        public CmsPageControl[] RegionControls
        {
            get
            {
                if (_regionControls == null)
                {
                    _regionControls = CmsTemplateService.GetTemplateRegionPageControls(Id);
                }

                return _regionControls;
            }
            set
            {
                _regionControls = value;
            }
        }

        public bool HasControls => (RegionControls != null) && (RegionControls.Length > 0);
    }
}
