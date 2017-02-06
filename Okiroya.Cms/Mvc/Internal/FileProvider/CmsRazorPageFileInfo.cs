using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using Okiroya.Campione.SystemUtility;
using System;
using System.IO;
using System.Text;

namespace Okiroya.Cms.Mvc.Internal.FileProvider
{
    public class CmsRazorPageFileInfo : IFileInfo
    {
        private string _viewName;
        private byte[] _pageBody;
        private bool _isExists;
        private DateTime _lastModified = DateTime.UtcNow;

        public CmsRazorPageFileInfo(string viewName)
        {
            Guard.ArgumentNotEmpty(viewName);

            _viewName = viewName;

            Init();
        }

        public bool Exists => _isExists;

        public bool IsDirectory => false;

        public DateTimeOffset LastModified => _lastModified;

        public long Length => _pageBody.Length;

        public string Name => _viewName;

        public string PhysicalPath => null;

        public Stream CreateReadStream()
        {
            return new MemoryStream(_pageBody ?? new byte[0]);
        }

        public static int ConvertViewNameToPageId(string viewName)
        {
            Guard.ArgumentNotEmpty(viewName);

            int pageId = 0;

            var nameIndex = viewName.IndexOf(CmsRazorPageFileProvider.CmsRazorPageName);
            var fileExtIndex = viewName.IndexOf(RazorViewEngine.ViewExtension);
            if ((nameIndex >= 0) && (fileExtIndex > nameIndex))
            {
                int.TryParse(
                    s: viewName.Substring(nameIndex, fileExtIndex - nameIndex).Replace($"{CmsRazorPageFileProvider.CmsRazorPageName}:", string.Empty),
                    result: out pageId);
            }

            return pageId;
        }

        private void Init()
        {
            var pageId = ConvertViewNameToPageId(_viewName);

            var container = CmsPageInfoStorage.GetTemplatePath(pageId);

            _isExists = !string.IsNullOrEmpty(container.Key);
            if (_isExists)
            {
                _lastModified = container.Value != DateTime.MinValue ?
                    container.Value :
                    DateTime.UtcNow;

                _pageBody = Encoding.UTF8.GetBytes(string.Concat("@{ Layout = \"", container.Key, "\"; }"));
            }
        }
    }
}
