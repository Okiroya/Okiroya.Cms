using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain
{
    public interface IWorkflowableEntity<TKey> : IEntityObject<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        Guid WorkflowId { get; set; }

        bool IsLatestCompletedRevision { get; set; }
    }
}
