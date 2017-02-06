using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okiroya.Cms.Domain.Meta
{
    public abstract class CmsMetaEntity<TKey> : CmsEntity<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private Dictionary<string, CmsMetaEntityItem[]> _metas;

        public CmsMetaEntityItem[] MetaItems
        {
            get
            {
                if (_metas == null)
                {
                    _metas = new Dictionary<string, CmsMetaEntityItem[]>(); //TODO: load
                }

                return _metas.Values.SelectMany(p => p).ToArray();
            }
            set
            {
                if (value == null)
                {
                    _metas = new Dictionary<string, CmsMetaEntityItem[]>();
                }
                else
                {
                    _metas = value
                        .GroupBy(p => p.FieldName, p => p)
                        .ToDictionary(p => p.Key, p => p.ToArray());
                }
            }
        }

        public T GetMetaValue<T>(string metaFieldName, Func<IDictionary<string, object>, T> mapper = null)
        {
            Guard.ArgumentNotEmpty(metaFieldName);

            CmsMetaEntityItem[] data;
            if (_metas.TryGetValue(metaFieldName, out data))
            {
                if (mapper == null)
                {
                    mapper = (p) => { return (T)p.Values.FirstOrDefault(); };
                }

                return mapper(MetaPropertyProvider.ConvertDataToRecord(data));
            }

            return default(T);
        }
    }
}
