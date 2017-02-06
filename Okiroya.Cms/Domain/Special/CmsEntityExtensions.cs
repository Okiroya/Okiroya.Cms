using Okiroya.Campione.SystemUtility;
using System;

namespace Okiroya.Cms.Domain
{
    public static class CmsEntityExtensions
    {
        public static bool IsCurrentlyPublished<TKey>(this IPublishableEntity<TKey> entity) where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            Guard.ArgumentNotNull(entity);

            var currentDate = DateTime.Now;

            return entity.IsPublished ?
                (currentDate >= entity.DatePublished()) && (currentDate <= entity.PublishEndDate.GetValueOrDefault(DateTime.MaxValue)) :
                false;
        }

        public static DateTime DatePublished<TKey>(this IPublishableEntity<TKey> entity) where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            Guard.ArgumentNotNull(entity);

            return entity.PublishStartDate.HasValue ?
                entity.PublishStartDate.Value :
                entity.CreatedDate;
        }

        public static string PublicationDates<TKey>(this IPublishableEntity<TKey> entity, string dateFormat = "dd.MM.yyyy") where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            Guard.ArgumentNotNull(entity);

            string result = entity.DatePublished().ToString(dateFormat);

            if (entity.PublishEndDate.HasValue)
            {
                result = string.Format($"{result} - {entity.PublishEndDate.Value.ToString(dateFormat)}");
            }

            return result;
        }
    }
}
