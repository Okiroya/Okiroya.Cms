using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Domain;
using Okiroya.Cms.Mvc.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okiroya.Cms.Mvc.View
{
    public abstract class CmsBasePage<TModel> : RazorPage<TModel>
    {
        private bool _isInited;
        private Dictionary<string, CmsPageControl[]> _pageControls = new Dictionary<string, CmsPageControl[]>();

        public CmsBasePage()
            : base()
        { }

        protected virtual IHtmlContent RenderRegion(string name)
        {
            var task = RenderRegionAsync(name);

            return task.GetAwaiter().GetResult();
        }

        protected virtual async Task<IHtmlContent> RenderRegionAsync(string name)
        {
            Guard.ArgumentNotEmpty(name);

            EnsureInit();

            var controls = _pageControls.ContainsKey(name) ?
                _pageControls[name] :
                null;

            if (controls != null)
            {
                var htmlHelper = Context.RequestServices.GetRequiredService<IHtmlHelper>();
                (htmlHelper as IViewContextAware).Contextualize(ViewContext);

                var viewComponentHelper = Context.RequestServices.GetRequiredService<IViewComponentHelper>();
                (viewComponentHelper as IViewContextAware).Contextualize(ViewContext);

                var cmsContext = Context.GetCmsContext();

                for (int i = 0; i < controls.Length; i++)
                {
                    await CmsPageControlFactory.ExecuteControlAsync(name, controls[i], Output, HtmlEncoder, htmlHelper, viewComponentHelper, cmsContext);
                }
            }

            return HtmlString.Empty;
        }

        private void EnsureInit()
        {
            if (_isInited)
            {
                return;
            }

            try
            {
                var cmsPage = Model as CmsPage;
                if (cmsPage != null)
                {
                    var cmsPageTemplateRegions = cmsPage.Template.HasRegions ?
                        cmsPage.Template.TemplateRegions :
                        new CmsTemplateRegion[0];

                    for (int i = 0; i < cmsPageTemplateRegions.Length; i++)
                    {
                        if (cmsPageTemplateRegions[i].HasControls && !_pageControls.ContainsKey(cmsPageTemplateRegions[i].Name))
                        {
                            _pageControls.Add(cmsPageTemplateRegions[i].Name, cmsPageTemplateRegions[i].RegionControls);
                        }
                    }
                }
            }
            finally
            {
                _isInited = true;
            }
        }
    }
}
