using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain
{
    public abstract class CmsABTestEntity<TKey> : BaseEntityObject<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public int? TestId { get; set; }

        public string ABGroup { get; set; }

        public bool IsABTest => TestId.HasValue;
    }
}
