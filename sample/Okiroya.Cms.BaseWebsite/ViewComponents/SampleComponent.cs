using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Okiroya.Cms.BaseWebsite.ViewComponents
{
    [ViewComponent(Name = "SampleComponent")]
    public class SampleComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int count, string message)
        {
            await Task.CompletedTask;

            return View(
                new SampleComponentViewModel
                {
                    Count = count,
                    Message = message ?? string.Empty
                });
        }
    }
}
