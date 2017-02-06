using Microsoft.AspNetCore.Rewrite;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Service.Rewrite;
using System;

namespace Okiroya.Cms.Mvc.DependencyInjection
{
    public static class CmsRewriteOptionsExtensions
    {
        public static RewriteOptions AddCmsRewrite(this RewriteOptions options)
        {
            Guard.ArgumentNotNull(options);

            options.Add(new CmsRewriteRuleRequest());

            return options;
        }
    }
}
