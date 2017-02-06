using Okiroya.Campione;
using Okiroya.Campione.Service;
using Okiroya.Campione.Service.Paging;
using Okiroya.Campione.SystemUtility.DI;
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
    public static class CmsContentService
    {
        static CmsContentService()
        {
            RegisterDependencyContainer<IEntityService<CmsContent, int>>.SetDefault(CmsServiceOptions.CmsScope, new CmsContentEntityService());
        }

        public static CmsContent GetCmsContent(int commonContentId, int siteId, byte abGroup)
        {
            var data = EntityServiceFacade<CmsContent, int>.ExecuteTypedQuery(
                commandName: CmsCommonCommandNames.CmsContentGetForFrontCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("CommonContentId", commonContentId);
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }));

            return data.DataResult
                .OrderByDescending(p => p.IsABTest)
                .ItemResolver<CmsContent, int>(abGroup);
        }

        public static async Task<CmsContent> GetCmsContentAsync(int commonContentId, int siteId, byte abGroup, CancellationToken token)
        {
            var data = await EntityServiceFacade<CmsContent, int>.ExecuteTypedQueryAsync(
                commandName: CmsCommonCommandNames.CmsContentGetForFrontCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("CommonContentId", commonContentId);
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }),
                cancellationToken: token)
                .ConfigureAwait(false);

            return data.DataResult
                .OrderByDescending(p => p.IsABTest)
                .ItemResolver<CmsContent, int>(abGroup);
        }

        public static CmsContent[] FindCmsContentForContentType(int contentTypeId, int siteId, byte abGroup)
        {
            var data = EntityServiceFacade<CmsContent, int>.ExecuteTypedQuery(
                commandName: CmsCommonCommandNames.CmsContentFindForContentTypeCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("ContentTypeId", contentTypeId);
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }));

            return data.DataResult.ItemsResolver<CmsContent, int>(abGroup);
        }

        public static async Task<CmsContent[]> FindCmsContentForContentTypeAsync(int contentTypeId, int siteId, byte abGroup, CancellationToken token)
        {
            var data = await EntityServiceFacade<CmsContent, int>.ExecuteTypedQueryAsync(
                commandName: CmsCommonCommandNames.CmsContentFindForContentTypeCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("ContentTypeId", contentTypeId);
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }),
                cancellationToken: token);

            return data.DataResult.ItemsResolver<CmsContent, int>(abGroup);
        }

        public static PagedCollection<CmsContent> FindCmsContentForContentTypePaged(int contentTypeId, int siteId, byte abGroup, int page = 1, int pageSize = 0)
        {
            var data = EntityServiceFacade<CmsContent, int>.ExecutePagedQuery(
                commandName: CmsCommonCommandNames.CmsContentFindForContentTypeWithPagingCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("ContentTypeId", contentTypeId);
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }),
                page: page,
                pageSize: pageSize,
                paged: true,
                sortable: false);

            return data.ItemsResolver<CmsContent, int>(abGroup);
        }

        public static async Task<PagedCollection<CmsContent>> FindCmsContentForContentTypePagedAsync(int contentTypeId, int siteId, byte abGroup, CancellationToken token, int page = 1, int pageSize = 0)
        {
            var data = await EntityServiceFacade<CmsContent, int>.ExecutePagedQueryAsync(
                commandName: CmsCommonCommandNames.CmsContentFindForContentTypeWithPagingCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("ContentTypeId", contentTypeId);
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }),
                cancellationToken: token,
                page: page,
                pageSize: pageSize,
                paged: true,
                sortable: false)
                .ConfigureAwait(false);

            return data.ItemsResolver<CmsContent, int>(abGroup);
        }
    }
}
