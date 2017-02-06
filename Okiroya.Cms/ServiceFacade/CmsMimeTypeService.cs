using Okiroya.Cms.Domain;
using System;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsMimeTypeService
    {
        public static readonly CmsMimeType DefaultMimeType = new CmsMimeType { ContentType = "application/octet-stream" };

        public static CmsMimeType GetMimeType(int? id)
        {
            return DefaultMimeType;
        }
    }
}
