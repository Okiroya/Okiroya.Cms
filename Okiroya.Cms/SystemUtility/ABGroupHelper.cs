using Okiroya.Campione.Service.Paging;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okiroya.Cms.SystemUtility
{
    public static class ABGroupHelper
    {
        public static PagedCollection<TItem> ItemsResolver<TItem, TKey>(this PagedCollection<TItem> source, byte abGroup)
            where TItem : CmsABTestEntity<TKey>
            where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            Guard.ArgumentNotNull(source);

            var result = new List<TItem>();

            foreach (var item in source)
            {
                if ((item.IsABTest && IsInGroup(item.ABGroup, abGroup) ||
                    !item.IsABTest))
                {
                    result.Add(item);
                }
            }

            return new PagedCollection<TItem>(result);
        }

        public static TItem[] ItemsResolver<TItem, TKey>(this IEnumerable<TItem> source, byte abGroup)
            where TItem : CmsABTestEntity<TKey>
            where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            Guard.ArgumentNotNull(source);

            var result = new List<TItem>();

            foreach (var item in source)
            {
                if ((item.IsABTest && IsInGroup(item.ABGroup, abGroup) ||
                    !item.IsABTest))
                {
                    result.Add(item);
                }
            }

            return result.ToArray();
        }

        public static TItem ItemResolver<TItem, TKey>(this IOrderedEnumerable<TItem> source, byte abGroup)
            where TItem : CmsABTestEntity<TKey>
            where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            Guard.ArgumentNotNull(source);

            foreach (var item in source)
            {
                if (item.IsABTest)
                {
                    if (IsInGroup(item.ABGroup, abGroup))
                    {
                        return item;
                    }
                }
                else
                {
                    return item;
                }
            }

            return null;
        }

        public static bool IsInGroup(string abGroupInterval, byte abGroup)
        {
            Guard.ArgumentNotEmpty(abGroupInterval);

            var parts = abGroupInterval.Replace(" ", string.Empty).Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int separatorIndex = 0;
            byte lowerValue = byte.MinValue, upperValue = byte.MinValue;
            for (int i = 0; i < parts.Length; i++)
            {
                separatorIndex = parts[i].IndexOf("-");
                if (separatorIndex > 0)
                {
                    if (byte.TryParse(parts[i].Substring(0, separatorIndex), out lowerValue) &&
                        byte.TryParse(parts[i].Substring(separatorIndex + 1, parts[i].Length - separatorIndex - 1), out upperValue) &&
                        ((abGroup >= lowerValue) && (abGroup <= upperValue)))
                    {
                        return true;
                    }
                }
                else if (byte.TryParse(parts[i], out lowerValue) &&
                    (abGroup == lowerValue))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
