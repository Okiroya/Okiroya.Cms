using System;
using Okiroya.Cms.Domain.Property;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsPropertyService
    {
        public static CmsPropertyEntityItem[] GetProperties<TKey>(TKey id, string entityTypeSysName) where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            throw new NotImplementedException();
        }
    }
}
