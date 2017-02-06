using Okiroya.Cms.Domain;
using System;

namespace Okiroya.Cms.API
{
    public interface IContentMapper<T> where T : class
    {
        T MapFromContent(CmsContent content);
    }
}
