using System;

namespace Okiroya.Cms.Domain.MediaFile
{
    public class CmsMediaViewItem
    {
        public int MediaContentId { get; set; }

        public int MediaFileId { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public string FileUrl { get; set; }

        public bool IsMain { get; set; }
    }
}
