using Okiroya.Campione;
using Okiroya.Campione.Service;
using Okiroya.Campione.SystemUtility.DI;
using Okiroya.Campione.SystemUtility.Extensions;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Domain;
using Okiroya.Cms.Mvc.DependencyInjection;
using Okiroya.Cms.Service;
using Okiroya.Cms.SystemUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsViewModuleService
    {
        static CmsViewModuleService()
        {
            RegisterDependencyContainer<IEntityService<CmsViewModule, int>>.SetDefault(CmsServiceOptions.CmsScope, new CmsViewModuleEntityService());
        }

        public static CmsViewModule GetCmsViewPartial(int commonId, int pageControlId, byte abGroup)
        {
            return GetCmsViewPartialAsync(commonId, pageControlId, abGroup, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static async Task<CmsViewModule> GetCmsViewPartialAsync(int commonId, int pageControlId, byte abGroup, CancellationToken token)
        {
            return await InnerGetCmsViewComponentAsync(commonId, pageControlId, false, abGroup, token);
        }

        public static CmsViewModule GetCmsViewComponent(int commonId, int pageControlId, byte abGroup)
        {
            return GetCmsViewComponentAsync(commonId, pageControlId, abGroup, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static async Task<CmsViewModule> GetCmsViewComponentAsync(int commonId, int pageControlId, byte abGroup, CancellationToken token)
        {
            return await InnerGetCmsViewComponentAsync(commonId, pageControlId, true, abGroup, token);
        }

        public static CmsViewModuleData[] FindCmsViewModuleData(int moduleId)
        {
            return EntityServiceFacade<CmsViewModuleData, int>.ExecuteTypedQuery(
                commandName: CmsCommonCommandNames.CmsViewComponentDataFindForViewComponentCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("Id", moduleId);
                        }))
                .DataResult
                .SafeToArray();
        }

        private static async Task<CmsViewModule> InnerGetCmsViewComponentAsync(int commonId, int pageControlId, bool withModel, byte abGroup, CancellationToken token)
        {
            var data = await EntityServiceFacade<CmsViewModule, int>.ExecuteTypedQueryAsync(
                commandName: CmsCommonCommandNames.CmsViewComponentGetForFrontCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("CommonId", commonId);
                            p.Add("PageControlId", pageControlId);
                            p.Add("WithModel", withModel);                            
                            p.Add("CurrentDate", DateTime.Now);
                        }),
                cancellationToken: token);

            return data.DataResult
                .OrderByDescending(p => p.IsABTest)
                .ItemResolver<CmsViewModule, int>(abGroup);
        }
    }
}
