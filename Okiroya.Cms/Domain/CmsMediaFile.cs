using Okiroya.Campione.Domain;
using Okiroya.Cms.Domain.Special;
using Okiroya.Cms.ServiceFacade;
using System;
using System.IO;

namespace Okiroya.Cms.Domain
{
    public class CmsMediaFile : Int32EntityObject
    {
        private CmsMimeType _mimeType;

        public int MediaId { get; set; }

        public int MediaContentId { get; set; }

        public int? MimeTypeId { get; set; }

        public int SiteGroupId { get; set; }

        public int? SiteId { get; set; }

        public string FileName { get; set; }

        public string LinkedFileName { get; set; }

        public long FileSize { get; set; }

        public bool IsMain { get; set; }

        public CmsMimeType MimeType
        {
            get
            {
                if (_mimeType == null)
                {
                    _mimeType = CmsMimeTypeService.GetMimeType(MimeTypeId);
                }

                return _mimeType;
            }
            set
            {
                _mimeType = value;
            }
        }

        public string Size => FileSize.ToFormattedFileSize();

        public string FileExtension => Path.GetExtension(LinkedFileName);

        public virtual string FrontEndUrl => $"/mediafiles/{MediaId}/{MediaContentId}/{Id}_{LinkedFileName}";

        public bool IsImage => MimeType.IsImage();

        public string ContentType => (MimeType ?? CmsMimeTypeService.DefaultMimeType).ContentType;

        public string FileType
        {
            get
            {
                var parts = ContentType.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                return parts.Length > 1 ? parts[1] : string.Empty;
            }
        }
    }
}
