using Okiroya.Cms.Domain.Property;
using System;

namespace Okiroya.Cms.Domain
{
    public abstract class CmsEntity<TKey> : CmsPropertyEntity<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public TKey CommonId { get; set; }

        public int SiteGroupId { get; set; }

        public int? SiteId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        bool IsDeleted { get; set; }

        public CmsEntity()
            : base()
        { }
    }
}
