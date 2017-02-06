using Okiroya.Campione.DataAccess;
using Okiroya.Campione.Service;
using Okiroya.Campione.Service.Dynamic;
using Okiroya.Campione.SystemUtility;
using Okiroya.Campione.SystemUtility.Extensions;
using Okiroya.Cms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okiroya.Cms.Service
{
    public sealed class CmsMediaContentEntityService : BaseEntityService<CmsMediaContent, int>
    {
        protected override IEnumerable<CmsMediaContent> ConvertDataItems(string commandName, IDictionary<string, object> parameters, IEnumerable<DataItem> items)
        {
            Guard.ArgumentNotNull(items);

            var mediaContents = base.ConvertDataItems(commandName, parameters, items.Where(p => p.Index == 0)).SafeToArray();

            for (int i = 0; i < mediaContents.Length; i++)
            {
                mediaContents[i].Files = items.Where(p => p.Index == 1)
                    .EnumerateDataItems(p => EntityObjectGenerator<int>.CreateEntityObjectFromMeta<CmsMediaFile>(p.Items))
                    .Where(p => p.MediaContentId == mediaContents[i].Id)
                    .SafeToArray();
            }

            return mediaContents;
        }
    }
}
