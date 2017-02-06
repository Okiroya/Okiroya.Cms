using Okiroya.Campione.Domain;
using Okiroya.Cms.ServiceFacade;
using System;

namespace Okiroya.Cms.Domain
{
    public class CmsSite : Int32EntityObject
    {
        private CmsSiteGroup _cmsSiteGroup;

        public int SiteGroupId { get; set; }

        public string Name { get; set; }

        public Guid ProjectId { get; set; }

        public CmsSiteGroup SiteGroup
        {
            get
            {
                if (_cmsSiteGroup == null)
                {
                    _cmsSiteGroup = CmsSiteService.GetSiteGroup(SiteGroupId);
                }

                return _cmsSiteGroup;
            }
            set
            {
                _cmsSiteGroup = value;
            }
        }
    }
}
