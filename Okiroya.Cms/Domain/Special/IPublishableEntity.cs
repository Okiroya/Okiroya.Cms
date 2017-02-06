using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain
{
    public interface IPublishableEntity<TKey> : IEntityObject<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        DateTime CreatedDate { get; set; }

        DateTime? PublishStartDate { get; set; }

        DateTime? PublishEndDate { get; set; }

        bool IsPublished { get; set; }
    }
}
