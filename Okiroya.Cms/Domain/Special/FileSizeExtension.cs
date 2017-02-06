using Okiroya.Cms.SystemUtility;
using System;

namespace Okiroya.Cms.Domain.Special
{
    public static class FileSizeExtension
    {
        private static IFormatProvider _defaultFileSizeProvider = new FileSizeFormatProvider();

        public static string ToFormattedFileSize(this long fileSize)
        {
            return string.Format(_defaultFileSizeProvider, "{0:" + FileSizeFormatProvider.FileSizeFormat + "}", fileSize);
        }
    }
}
