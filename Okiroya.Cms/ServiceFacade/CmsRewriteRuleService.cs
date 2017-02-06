using Okiroya.Campione;
using Okiroya.Campione.Service;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Domain;
using Okiroya.Cms.SystemUtility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsRewriteRuleService
    {
        public static CmsRewriteRule GetForUrl(string url, int siteId, byte abGroup)
        {
            Guard.ArgumentNotEmpty(url);

            var data = EntityServiceFacade<CmsRewriteRule, int>.ExecuteTypedQuery(
                commandName: CmsCommonCommandNames.CmsRewriteRuleGetForFrontCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("Url", url);
                            p.Add("SiteId", siteId);
                            p.Add("CurrentDate", DateTime.Now);
                        }));

            return data.DataResult
                .OrderByDescending(p => p.IsABTest)
                .ItemResolver<CmsRewriteRule, int>(abGroup);
        }
    }
}
