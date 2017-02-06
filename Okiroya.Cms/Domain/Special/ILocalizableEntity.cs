using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain
{
    public interface ILocalizableEntity<TKey> : IEntityObject<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        int? LocaleId { get; set; }
    }
}
