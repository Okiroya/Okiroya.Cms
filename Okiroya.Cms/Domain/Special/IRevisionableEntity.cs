using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain
{
    public interface IRevisionableEntity<TKey> : IEntityObject<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        bool IsLatestRevision { get; set; }

        bool IsLatestCompletedRevision { get; set; }
    }
}
