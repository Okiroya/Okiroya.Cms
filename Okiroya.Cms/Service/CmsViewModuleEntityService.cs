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
    public sealed class CmsViewModuleEntityService : BaseEntityService<CmsViewModule, int>
    {
        protected override IEnumerable<CmsViewModule> ConvertDataItems(string commandName, IDictionary<string, object> parameters, IEnumerable<DataItem> items)
        {
            Guard.ArgumentNotNull(items);

            var result = base.ConvertDataItems(commandName, parameters, items.Where(p => p.Index == 0)).SafeToArray();

            if ((parameters != null) && (bool)parameters["WithModel"])
            {
                var data = items
                    .Where(p => p.Index == 1)
                    .EnumerateDataItems(p => EntityObjectGenerator<int>.CreateEntityObjectFromMeta<CmsViewModuleData>(p.Items))
                    .SafeToArray();

                foreach (var item in result)
                {
                    item.RawData = data
                        .Where(p => p.ModuleId == item.Id)
                        .SafeToArray();
                }
            }

            return result;
        }
    }
}
