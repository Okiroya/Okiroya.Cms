using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Domain;
using Okiroya.Cms.ServiceFacade;
using Okiroya.Cms.SystemUtility;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace Okiroya.Cms.Mvc.Internal
{
    internal static class CmsPageControlFactory
    {
        public static async Task ExecuteControlAsync(string regionName, CmsPageControl pageControl, TextWriter writer, HtmlEncoder encoder, IHtmlHelper helper, IViewComponentHelper componentHelper, ICmsContext context)
        {
            Guard.ArgumentNotEmpty(regionName);
            Guard.ArgumentNotNull(pageControl);
            Guard.ArgumentNotNull(writer);
            Guard.ArgumentNotNull(encoder);
            Guard.ArgumentNotNull(helper);
            Guard.ArgumentNotNull(componentHelper);
            Guard.ArgumentNotNull(context);

            try
            {
                switch (pageControl.ControlType)
                {
                    case CmsPageControlTypes.Content:
                        await RenderContentAsync(pageControl.ControlContentId, writer, context);
                        break;
                    case CmsPageControlTypes.ViewModule:
                        await RenderControlAsync(pageControl.ControlViewComponentId, pageControl.PageControlId, componentHelper, writer, encoder, context);
                        break;

                    case CmsPageControlTypes.PartialView:
                        await RenderPartialAsync(pageControl.ControlViewComponentId, pageControl.PageControlId, helper, context);
                        break;
                }
            }
            catch (Exception ex)
            {
                if (!context.IsDebugEnabled)
                {
                    await RenderControlErrorAsync(pageControl, ex, writer, encoder);
                }
                else
                {
                    throw;
                }
            }
        }

        private static Task RenderControlErrorAsync(CmsPageControl pageControl, Exception ex, TextWriter writer, HtmlEncoder encoder)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.TagRenderMode = TagRenderMode.Normal;

            tagBuilder.MergeAttribute("style", "color: #ff0000; border: 1px dashed #ff0000; padding: 5px;", true);
            tagBuilder.InnerHtml.SetHtmlContent(
                string.Format("<p>Okiroya.Cms runtime error: <br/>Error loading control. PageControlId = {0}; <br/> Exception = {1}</p>",
                    pageControl.Id,
                    ex.InnerException != null ? $"{ex.Message} : {ex.InnerException}" : ex.Message));

            tagBuilder.WriteTo(writer, encoder);

            return Task.CompletedTask;
        }

        private static async Task RenderContentAsync(int contentId, TextWriter writer, ICmsContext context)
        {
            var content = await CmsContentService.GetCmsContentAsync(contentId, context.SiteId, context.CurrentABGroup, CancellationToken.None);

            await writer.WriteLineAsync(content?.Body);
        }

        private static async Task RenderControlAsync(int controlViewComponentId, int pageControlId, IViewComponentHelper componentHelper, TextWriter writer, HtmlEncoder encoder, ICmsContext context)
        {
            var module = await CmsViewModuleService.GetCmsViewComponentAsync(controlViewComponentId, pageControlId, context.CurrentABGroup, CancellationToken.None);

            if (module != null)
            {
                IHtmlContent html = await componentHelper.InvokeAsync(module.ComponentPath, module.Data);

                html.WriteTo(writer, encoder);
            }
            else
            {
                //TODO: log
            }
        }

        private static async Task RenderPartialAsync(int controlViewComponentId, int pageControlId, IHtmlHelper helper, ICmsContext context)
        {
            var partial = await CmsViewModuleService.GetCmsViewPartialAsync(controlViewComponentId, pageControlId, context.CurrentABGroup, CancellationToken.None);

            if (partial != null)
            {
                await helper.RenderPartialAsync(partial.ComponentPath);
            }
            else
            {
                //TODO: log
            }
        }
    }
}
