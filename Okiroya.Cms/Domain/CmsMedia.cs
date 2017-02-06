using Okiroya.Campione.Domain;
using Okiroya.Cms.ServiceFacade;
using System;

namespace Okiroya.Cms.Domain
{
    public class CmsMedia : Int32EntityObject
    {
        private CmsMediaContent[] _mediaContents;

        public string Name { get; set; }

        public string Description { get; set; }

        public int SiteGroupId { get; set; }

        public int? SiteId { get; set; }

        public CmsMediaContent[] MediaContents
        {
            get
            {
                if (_mediaContents == null)
                {
                    _mediaContents = CmsMediaContentService.GetMediaContents(Id);
                }

                return _mediaContents;
            }
            set
            {
                _mediaContents = value;
            }
        }

        public int TotalMediaContent => MediaContents != null ? MediaContents.Length : 0;
    }
}
