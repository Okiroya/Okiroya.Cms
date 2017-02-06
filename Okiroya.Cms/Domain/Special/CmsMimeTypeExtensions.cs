using Okiroya.Campione.SystemUtility;
using System;

namespace Okiroya.Cms.Domain.Special
{
    public static class CmsMimeTypeExtensions
    {
        public static bool IsImage(this CmsMimeType cmsMimeType)
        {
            Guard.ArgumentNotNull(cmsMimeType);

            return true;
        }
        public static bool IsVideo(this CmsMimeType cmsMimeType)
        {
            Guard.ArgumentNotNull(cmsMimeType);

            return true;
        }

        public static bool IsDocument(this CmsMimeType cmsMimeType)
        {
            Guard.ArgumentNotNull(cmsMimeType);

            return true;
        }
    }
}
