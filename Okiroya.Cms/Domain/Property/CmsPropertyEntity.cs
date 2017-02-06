using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Service;
using Okiroya.Cms.ServiceFacade;
using System;
using System.Collections.Generic;

namespace Okiroya.Cms.Domain.Property
{
    public abstract class CmsPropertyEntity<TKey> : CmsABTestEntity<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private CmsPropertyEntityItem[] _properties;
        private Dictionary<string, object> _propertyMap;

        public CmsPropertyEntityItem[] Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = CmsPropertyService.GetProperties(Id, EntityTypeSysName);

                    if (_properties != null)
                    {
                        _propertyMap = new Dictionary<string, object>(MetaPropertyProvider.ConvertPropertiesToDictionary(_properties));
                    }
                }

                return _properties;
            }
            set
            {
                _properties = value;

                if (_properties != null)
                {
                    _propertyMap = new Dictionary<string, object>(MetaPropertyProvider.ConvertPropertiesToDictionary(_properties));
                }
            }
        }

        public T GetProperty<T>(string propertyName)
        {
            Guard.ArgumentNotEmpty(propertyName);

            object tmp = null;

            return _propertyMap.TryGetValue(propertyName, out tmp) ?
                (T)tmp :
                default(T);
        }
    }
}
