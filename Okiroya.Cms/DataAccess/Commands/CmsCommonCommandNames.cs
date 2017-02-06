using Okiroya.Campione.DataAccess;
using Okiroya.Campione.SystemUtility;
using Okiroya.Campione.SystemUtility.DI;
using System;

namespace Okiroya.Cms.DataAccess.Commands
{
    public static class CmsCommonCommandNames
    {
        public const string CmsEntityTypeDefinitionGetAllCommandName = "sp_EntityTypeDefinitionGetAll";

        public const string CmsSiteGroupGetCommandName = "sp_SiteGroupGet";

        public const string CmsTemplateGetCommandName = "sp_TemplateGet";

        public const string CmsTemplateRegionFindForTemplateCommandName = "sp_TemplateRegionFindForTemplate";

        public const string CmsPageControlFindForTemplateRegionCommandName = "sp_PageControlFindForTemplateRegion";

        public const string CmsPageGetForFrontCommandName = "sp_PageGetForFront";

        public const string CmsPageInfoFindFromCache = "cache_PageInfoFind";

        public const string CmsPageInfoFind = "sp_PageInfoFind";

        public const string CmsPageAdd = "sp_PageAdd";

        public const string CmsContentGetForFrontCommandName = "sp_ContentGetForFront";

        public const string CmsContentFindForContentTypeCommandName = "sp_ContentFindByContentTypeId";

        public const string CmsContentFindForContentTypeWithPagingCommandName = "sp_ContentFindByContentTypeIdWithPaging";

        public const string CmsContentTypeGetAllCommandName = "sp_ContentTypeGetAll";

        public const string CmsViewComponentGetForFrontCommandName = "sp_ViewComponentGetForFront";

        public const string CmsViewComponentDataFindForViewComponentCommandName = "sp_ViewComponentFindForViewComponent";

        public const string CmsMediaContentGetForFrontCommandName = "sp_MediaContentGetForFront";

        public const string CmsMenuGetHierarchy = "sp_MenuGetHierarchy";

        public const string CmsRewriteRuleGetForFrontCommandName = "sp_RewriteRuleGetForFront";

        public static void RegisterCommandsScope(string cmsScope, string administrationScope)
        {
            Guard.ArgumentNotEmpty(cmsScope);
            Guard.ArgumentNotEmpty(administrationScope);

            RegisterDependencyContainer.RegisterScope(CmsEntityTypeDefinitionGetAllCommandName, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsPageInfoFind, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsPageGetForFrontCommandName, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsContentGetForFrontCommandName, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsViewComponentGetForFrontCommandName, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsContentTypeGetAllCommandName, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsContentFindForContentTypeCommandName, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsContentFindForContentTypeWithPagingCommandName, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsMenuGetHierarchy, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsRewriteRuleGetForFrontCommandName, cmsScope);
            RegisterDependencyContainer.RegisterScope(CmsMediaContentGetForFrontCommandName, cmsScope); 

            RegisterDependencyContainer.RegisterScope(CmsSiteGroupGetCommandName, administrationScope);
            RegisterDependencyContainer.RegisterScope(CmsTemplateGetCommandName, administrationScope);
            RegisterDependencyContainer.RegisterScope(CmsTemplateRegionFindForTemplateCommandName, administrationScope);
            RegisterDependencyContainer.RegisterScope(CmsPageControlFindForTemplateRegionCommandName, administrationScope);
            RegisterDependencyContainer.RegisterScope(CmsPageAdd, administrationScope);
            RegisterDependencyContainer.RegisterScope(CmsViewComponentDataFindForViewComponentCommandName, administrationScope); 
        }
    }
}
