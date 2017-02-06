using Okiroya.Campione;
using Okiroya.Campione.Service;
using Okiroya.Campione.Service.Cache;
using Okiroya.Campione.SystemUtility;
using Okiroya.Campione.SystemUtility.DI;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Domain;
using Okiroya.Cms.Mvc.DependencyInjection;
using Okiroya.Cms.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsPageService
    {
        static CmsPageService()
        {
            //RegisterDependencyContainer<ICacheService>.Register(CmsCommonCommandNames.CmsPageInfoFindFromCache, cacheService);

            CachePolicyRegister.AddPolicy(CmsCommonCommandNames.CmsPageInfoFindFromCache, new CachePolicy { AbsolutExpiration = new TimeSpan(0, 5, 0) });

            DependencyCacheRegister.AddToDependcy(CmsCommonCommandNames.CmsPageAdd, CmsCommonCommandNames.CmsPageInfoFindFromCache);

            RegisterDependencyContainer<IEntityService<CmsPage, int>>.SetDefault(CmsServiceOptions.CmsScope, new CmsPageEntityService());
        }

        public static CmsPage GetCmsPage(CmsPageInfo pageInfo, byte abGroup)
        {
            Guard.ArgumentNotNull(pageInfo);

            return GetCmsPage(pageInfo.IsABTest ? pageInfo.Id : pageInfo.CommonId, pageInfo.SiteGroupId, pageInfo.SiteId, abGroup);
        }

        public static CmsPage GetCmsPage(int commonPageId, int siteGroupId, int? siteId, byte abGroup)
        {
            return EntityServiceFacade<CmsPage, int>.GetItem(
                commandName: CmsCommonCommandNames.CmsPageGetForFrontCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("CommonPageId", commonPageId);
                            p.Add("SiteGroupId", siteGroupId);
                            p.Add("SiteId", siteId.GetValueOrDefault(0));                            
                            p.Add("CurrentDate", DateTime.Now);
                            p.Add("AbGroup", abGroup);
                        }));
        }

        public static CmsPage[] GetAllCmsPages(int siteId)
        {
            return new[] { new CmsPage() };
        }

        public static CmsPageInfo[] GetCmsPageInfo(string url, int siteId)
        {
            Guard.ArgumentNotEmpty(url);

            CmsPageInfo[] result = null;

            if (!LoadCmsPageInfos(siteId).TryGetValue(url, out result))
            {
                //TODO: log not found
            }

            return result;
        }

        private static Dictionary<string, CmsPageInfo[]> LoadCmsPageInfos(int siteId) 
        {
            var parameters = new Dictionary<string, object>()
                .FluentIt(
                    p =>
                    {
                        p.Add("SiteId", siteId);
                        p.Add("CurrentDate", DateTime.Now);
                    });

            return CacheFacade.AddOrGetExisting(
                commandName: CmsCommonCommandNames.CmsPageInfoFindFromCache,
                parameters: parameters,
                dataLoader: () =>
                {
                    var data = EntityServiceFacade<CmsPageInfo, int>.ExecuteTypedQuery(
                        commandName: CmsCommonCommandNames.CmsPageInfoFind,
                        parameters: parameters)
                        .DataResult;

                    return data != null ?
                        data.GroupBy(p => p.Url, p => p).ToDictionary(p => p.Key, p => p.ToArray()) :
                        new Dictionary<string, CmsPageInfo[]>();
                });
        }
    }
}
