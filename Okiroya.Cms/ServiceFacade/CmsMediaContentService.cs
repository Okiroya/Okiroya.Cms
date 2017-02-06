using Okiroya.Campione;
using Okiroya.Campione.Service;
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
    public static class CmsMediaContentService
    {
        private static readonly CmsMediaContent[] _voidMediaContent = new CmsMediaContent[0];
        private static readonly CmsMediaFile[] _voidMediaFiles = new CmsMediaFile[0];

        static CmsMediaContentService()
        {
            RegisterDependencyContainer<IEntityService<CmsMediaContent, int>>.SetDefault(CmsServiceOptions.CmsScope, new CmsMediaContentEntityService());
        }

        public static CmsMediaContent GetMediaContent(int commonMediaContentId, int siteId, byte abGroup)
        {
            var data = EntityServiceFacade<CmsMediaContent, int>.ExecuteTypedQuery(
                commandName: CmsCommonCommandNames.CmsMediaContentGetForFrontCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("CommonId", commonMediaContentId);
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }));

            return data.DataResult
                .OrderByDescending(p => p.IsABTest)
                .ItemResolver<CmsMediaContent, int>(abGroup);
        }

        public static async Task<CmsMediaContent> GetMediaContentAsync(int commonMediaContentId, int siteId, byte abGroup, CancellationToken token)
        {
            var data = await EntityServiceFacade<CmsMediaContent, int>.ExecuteTypedQueryAsync(
                commandName: CmsCommonCommandNames.CmsMediaContentGetForFrontCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("CommonId", commonMediaContentId);
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }),
                cancellationToken: token)
                .ConfigureAwait(false);

            return data.DataResult
                .OrderByDescending(p => p.IsABTest)
                .ItemResolver<CmsMediaContent, int>(abGroup);
        }

        public static CmsMediaContent[] GetMediaContents(int mediaId)
        {
            return _voidMediaContent;
        }

        public static CmsMediaFile[] GetMediaFiles(int mediaContentId)
        {
            return _voidMediaFiles;
        }

        public static CmsMediaTag[] GetMediaTags(int mediaContentId)
        {
            throw new NotImplementedException();
        }
    }
}
