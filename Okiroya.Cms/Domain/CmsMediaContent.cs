using Okiroya.Cms.ServiceFacade;
using System;
using System.Linq;

namespace Okiroya.Cms.Domain
{
    public class CmsMediaContent : CmsEntity<int>
    {
        public const string CmsMediaContentEntityType = "CmsMediaContent";

        private CmsMediaFile[] _files;
        private CmsMediaTag[] _tags;

        public int MediaId { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public CmsMediaFile[] Files
        {
            get
            {
                if (_files == null)
                {
                    _files = CmsMediaContentService.GetMediaFiles(Id);
                }

                return _files;
            }
            set
            {
                _files = value;
            }
        }

        public CmsMediaFile MainFile => Files.FirstOrDefault(p => p.IsMain);

        public CmsMediaTag[] Tags
        {
            get
            {
                if (_tags == null)
                {
                    _tags = CmsMediaContentService.GetMediaTags(Id);
                }

                return _tags;
            }
            set
            {
                _tags = value;
            }
        }

        public CmsMediaContent()
            : base()
        {
            EntityTypeSysName = CmsMediaContentEntityType;
        }
    }
}
