using Microsoft.Extensions.Primitives;
using Okiroya.Campione.SystemUtility;
using System;

namespace Okiroya.Cms.Mvc.Internal.FileProvider
{
    public class CmsRazorPageChangeToken : IChangeToken
    {
        private int _pageId;
        private bool _hasChanged;

        public CmsRazorPageChangeToken(string viewName)
        {
            Guard.ArgumentNotEmpty(viewName);

            _pageId = CmsRazorPageFileInfo.ConvertViewNameToPageId(viewName);

            CmsPageInfoStorage.RegisterChangedCallback(ChangeCallback);
        }

        public bool ActiveChangeCallbacks => false;

        public bool HasChanged => _hasChanged;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state) => EmptyDisposable.Instance;

        private void ChangeCallback(int id)
        {
            _hasChanged = _pageId == id;
        }
    }

    internal class EmptyDisposable : IDisposable
    {
        public static EmptyDisposable Instance { get; } = new EmptyDisposable();

        private EmptyDisposable()
        { }

        public void Dispose()
        { }
    }
}
