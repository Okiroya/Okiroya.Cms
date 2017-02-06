using Okiroya.Campione.Service;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Domain.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsEntityTypeDefinitionRegistry
    {
        private static Lazy<Dictionary<string, int>> _map = new Lazy<Dictionary<string, int>>(LoadMap, LazyThreadSafetyMode.PublicationOnly);

        public static int GetEntityTypeId(string name)
        {
            Guard.ArgumentNotEmpty(name);

            int result = 0;
            _map.Value.TryGetValue(name, out result);

            return result;
        }

        private static Dictionary<string, int> LoadMap()
        {
            var data = EntityServiceFacade<CmsEntityTypeDefinition, int>
                .ExecuteTypedQuery(CmsCommonCommandNames.CmsEntityTypeDefinitionGetAllCommandName)
                .DataResult;

            return data != null ?
                data.ToDictionary(p => p.Name, p => p.Id) :
                new Dictionary<string, int>();
        }
    }
}
