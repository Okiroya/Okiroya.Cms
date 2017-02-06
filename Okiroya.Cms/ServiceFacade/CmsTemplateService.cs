using Okiroya.Campione;
using Okiroya.Campione.Service;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsTemplateService
    {
        public static CmsTemplate GetTemplate(int templateId)
        {
            return EntityServiceFacade<CmsTemplate, int>.GetItem(
                commandName: CmsCommonCommandNames.CmsTemplateGetCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("TemplateId", templateId);
                        }));
        }

        public static CmsTemplateRegion[] GetTemplateRegions(int templateId)
        {
            return EntityServiceFacade<CmsTemplateRegion, int>.ExecuteTypedQuery(
                commandName: CmsCommonCommandNames.CmsTemplateRegionFindForTemplateCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("TemplateId", templateId);
                        }))
               .DataResult
               .ToArray();
        }

        public static CmsPageControl[] GetTemplateRegionPageControls(int templateRegionId)
        {
            return EntityServiceFacade<CmsPageControl, int>.ExecuteTypedQuery(
                commandName: CmsCommonCommandNames.CmsPageControlFindForTemplateRegionCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("TemplateRegionId", templateRegionId);
                        }))
               .DataResult
               .ToArray();
        }
    }
}
