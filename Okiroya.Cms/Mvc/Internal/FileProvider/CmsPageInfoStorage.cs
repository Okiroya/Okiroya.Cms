using Okiroya.Campione.SystemUtility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okiroya.Cms.Mvc.Internal.FileProvider
{
    public static class CmsPageInfoStorage
    {
        private static ConcurrentDictionary<int, KeyValuePair<string, DateTime>> _templatePathMap = new ConcurrentDictionary<int, KeyValuePair<string, DateTime>>();        
        private static ConcurrentBag<Action<int>> _changedCallbacks = new ConcurrentBag<Action<int>>();

        private static KeyValuePair<string, DateTime> _empty = new KeyValuePair<string, DateTime>();

        public static void RegisterChangedCallback(Action<int> callback)
        {
            Guard.ArgumentNotNull(callback);

            _changedCallbacks.Add(callback);
        }

        public static void AddOrUpdate(int pageId, string templatePath)
        {
            var mapValue = new KeyValuePair<string, DateTime>(templatePath, DateTime.UtcNow);

            _templatePathMap.AddOrUpdate(
                key: pageId,
                addValue: mapValue,
                updateValueFactory:
                    (id, path) =>
                    {
                        if (!string.Equals(templatePath, path.Key, StringComparison.OrdinalIgnoreCase))
                        {
                            Task.Run(() => RaiseChangedCallback(id));
                        }

                        return mapValue;
                    });
        }

        public static KeyValuePair<string, DateTime> GetTemplatePath(int pageId)
        {
            KeyValuePair<string, DateTime> result = _empty;

            if (!_templatePathMap.TryGetValue(pageId, out result))
            {
                //TODO: не найдено
            }

            return result;
        }

        private static void RaiseChangedCallback(int id)
        {
            Action<int> callback;
            while (_changedCallbacks.TryPeek(out callback))
            {
                try
                {
                    callback?.Invoke(id);
                }
                catch
                {
                    //do nothing
                }
            }
        }
    }
}
