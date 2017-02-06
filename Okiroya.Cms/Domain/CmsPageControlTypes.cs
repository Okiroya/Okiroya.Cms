using System;

namespace Okiroya.Cms.Domain
{
    public enum CmsPageControlTypes : byte
    {
        Undefined = 0,
        Content = 1,
        Media = 2,
        ViewModule = 3,
        PartialView = 4,
        Module = 5
    }
}
