using Okiroya.Campione.DataAccess;
using Okiroya.Campione.Service;
using Okiroya.Campione.Service.Dynamic;
using Okiroya.Campione.SystemUtility;
using Okiroya.Campione.SystemUtility.Extensions;
using Okiroya.Cms.Domain;
using Okiroya.Cms.Domain.Meta;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okiroya.Cms.Service
{
    public sealed class CmsContentEntityService : BaseEntityService<CmsContent, int>
    {
        protected override IEnumerable<CmsContent> ConvertDataItems(string commandName, IDictionary<string, object> parameters, IEnumerable<DataItem> items)
        {
            Guard.ArgumentNotNull(items);

            var contents = base.ConvertDataItems(commandName, parameters, items.Where(p => p.Index == 0)).SafeToArray();

            for (int i = 0; i < contents.Length; i++)
            {
                contents[i].MetaItems = items.Where(p => p.Index == 1)
                    .EnumerateDataItems(p => EntityObjectGenerator<int>.CreateEntityObjectFromMeta<CmsMetaEntityItem>(p.Items))
                    .Where(p => p.EntityId == contents[i].Id)
                    .SafeToArray();
            }

            return contents;
        }
    }
}