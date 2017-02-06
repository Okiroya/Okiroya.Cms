using Okiroya.Campione;
using Okiroya.Campione.Service;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Domain;
using Okiroya.Cms.SystemUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsMenuService
    {
        public static CmsMenuItem[] GetMenuHierarchy(int siteId, byte abGroup)
        {
            return EntityServiceFacade<CmsMenuItem, int>.ExecuteTypedQuery(
                commandName: CmsCommonCommandNames.CmsMenuGetHierarchy,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }))
                .DataResult
                .OrderBy(p => p.ParentPageId)
                .ItemsResolver<CmsMenuItem, int>(abGroup);
        }

        public static async Task<CmsMenuItem[]> GetMenuHierarchyAsync(int siteId, byte abGroup, CancellationToken token)
        {
            var data = await EntityServiceFacade<CmsMenuItem, int>.ExecuteTypedQueryAsync(
                commandName: CmsCommonCommandNames.CmsMenuGetHierarchy,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }),
                cancellationToken: token)
                .ConfigureAwait(false);

            return data
                .DataResult
                .OrderBy(p => p.ParentPageId)
                .ItemsResolver<CmsMenuItem, int>(abGroup);
        }
    }
}
