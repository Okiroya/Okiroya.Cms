using Microsoft.AspNetCore.Mvc;
using Okiroya.Cms.API;
using Okiroya.Cms.BaseWebsite.Domain;
using Okiroya.Cms.Mvc;
using System;
using System.Threading.Tasks;

namespace Okiroya.Cms.BaseWebsite.ViewComponents
{
    [ViewComponent(Name = "ProductComponent")]
    public class ProductComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var product = new ProductModel(await App.GetCurrentSite(HttpContext.GetCmsContext()).WorkWith().Content().GetAsync(7));

            return View(product);
        }
    }
}
