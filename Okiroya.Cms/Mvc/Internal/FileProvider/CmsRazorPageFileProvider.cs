using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Okiroya.Campione.SystemUtility;
using System;

namespace Okiroya.Cms.Mvc.Internal.FileProvider
{
    public class CmsRazorPageFileProvider : IFileProvider
    {
        public const string CmsRazorPageName = "_CmsDefault";
        
        private IFileProvider _fileProvider;

        public CmsRazorPageFileProvider(IFileProvider fileProvider)
        {
            Guard.ArgumentNotNull(fileProvider);
            
            _fileProvider = fileProvider;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return _fileProvider.GetDirectoryContents(subpath);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return IsCmsRazorPage(subpath) ?
                new CmsRazorPageFileInfo(subpath) :
                _fileProvider.GetFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            if (!string.IsNullOrEmpty(filter) && IsCmsRazorPage(filter))
            {
                return new CmsRazorPageChangeToken(filter);
            }

            return _fileProvider.Watch(filter);
        }

        private bool IsCmsRazorPage(string path)
        {
            return (path ?? string.Empty).Contains(CmsRazorPageName);
        }
    }
}
