using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Okiroya.Cms.Domain;
using Okiroya.Cms.Mvc.DependencyInjection;
using Okiroya.Cms.Mvc.Internal;
using Okiroya.Cms.Mvc.Internal.FileProvider;
using Okiroya.Cms.ServiceFacade;
using Okiroya.Cms.SystemUtility;
using System;
using System.Text;

namespace Okiroya.Cms.Mvc.Controllers
{
    public class CmsController : Controller
    {
        private static readonly string _cmsPageKey = "CurrentCmsPage";

        public CmsController()
        { }

        public IActionResult Index()
        {
            IActionResult result = null;

            if (ControllerContext.RouteData.Values.ContainsKey(_cmsPageKey))
            {
                var cmsPageInfo = ControllerContext.RouteData.Values[_cmsPageKey] as CmsPageInfo;

                if (cmsPageInfo != null)
                {
                    //TODO log current view name

                    var cmsPage = CmsPageService.GetCmsPage(cmsPageInfo, ControllerContext.HttpContext.GetCurrentABGroup());

                    if (cmsPage != null)
                    {
                        CmsPageInfoStorage.AddOrUpdate(cmsPageInfo.Id, cmsPageInfo.TemplatePath);

                        ViewBag.Title = cmsPage.GetProperty<string>("Title") ?? cmsPage.Name;
                        ViewBag.Description = cmsPage.GetProperty<string>("Description") ?? string.Empty;

                        result = View($"{CmsRazorPageFileProvider.CmsRazorPageName}:{cmsPageInfo.Id}", cmsPage);
                    }
                    else
                    {
                        //TODO log 
                    }
                }
            }

            return result ?? Show404();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cmsContext = context.HttpContext.GetCmsContext();

            var cmsPages = CmsPageService.GetCmsPageInfo(Request.Path.ToUriComponent(), cmsContext.SiteId);

            if (cmsPages?.Length > 0)
            {
                byte currentABGroup = cmsContext.CurrentABGroup;

                CmsPageInfo currentPage = null;
                for (int i = 0; i < cmsPages.Length; i++)
                {
                    if (cmsPages[i].IsABTest)
                    {
                        if (ABGroupHelper.IsInGroup(cmsPages[i].ABGroup, currentABGroup))
                        {
                            currentPage = cmsPages[i];
                            break;
                        }
                    }
                    else
                    {
                        currentPage = cmsPages[i];
                    }
                }

                if (currentPage != null)
                {
                    context.RouteData.Values.Add(_cmsPageKey, currentPage);
                }
            }

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 404 страница
        /// </summary>
        /// <param name="showPartial"></param>
        /// <returns></returns>
        protected virtual ActionResult Show404(bool showPartial = false)
        {
            ControllerContext.HttpContext.Response.StatusCode = 404;

            return showPartial ?
                PartialView("404") as ActionResult :
                View("404");
        }

        /// <summary>
        /// 500 страница
        /// </summary>
        /// <param name="message"></param>
        /// <param name="textOnly"></param>
        /// <param name="showPartial"></param>
        /// <returns></returns>
        protected virtual ActionResult Show500(string message, bool textOnly = false, bool showPartial = true)
        {
            ActionResult result = null;

            ControllerContext.HttpContext.Response.StatusCode = 500;

            if (textOnly)
            {
                result = new ContentResult
                {
                    Content = message,
                    ContentType = Encoding.UTF8.WebName,
                    StatusCode = 500
                };
            }
            else
            {
                ModelState.AddModelError(string.Empty, message);

                result = showPartial ?
                    PartialView("500") as ActionResult :
                    View("500");
            }

            return result;
        }
    }
}
