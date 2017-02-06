using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Mvc;
using Okiroya.Cms.ServiceFacade;
using System;

namespace Okiroya.Cms.Service.Rewrite
{
    public sealed class CmsRewriteRuleRequest : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            Guard.ArgumentNotNull(context);

            var request = context.HttpContext.Request;

            if (request.Method == "GET")
            {
                var cmsContext = context.HttpContext.GetCmsContext();

                var rule = CmsRewriteRuleService.GetForUrl(request.Path.ToUriComponent(), cmsContext.SiteId, cmsContext.CurrentABGroup);
                if ((rule == null) ||
                    ((rule != null) && string.IsNullOrWhiteSpace(rule.NewUrl)))
                {
                    return;
                }

                var response = context.HttpContext.Response;

                if ((rule.StatusCode == StatusCodes.Status301MovedPermanently) ||
                    (rule.StatusCode == StatusCodes.Status302Found))
                {
                    response.StatusCode = rule.StatusCode;
                    response.Headers[HeaderNames.Location] = rule.NewUrl;

                    context.Result = RuleResult.EndResponse;
                }
                else if (rule.StatusCode == StatusCodes.Status200OK)
                {
                    var parts = rule.NewUrl.Split('?');

                    request.Path = new PathString(parts[0]);
                    if (parts.Length > 1)
                    {
                        request.QueryString = new QueryString($"?{parts[1]}");
                    }

                    context.Result = RuleResult.SkipRemainingRules;
                }
                else
                {
                    context.Result = RuleResult.ContinueRules;
                }
            }
        }
    }
}
