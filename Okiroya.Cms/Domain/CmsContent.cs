using Okiroya.Cms.Domain.Meta;
using System;

namespace Okiroya.Cms.Domain
{
    public class CmsContent : CmsMetaEntity<int>
    {
        public const string CmsContentEntityType = "CmsContent";

        public int ContentTypeId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Code { get; set; }

        public CmsContent()
            : base()
        {
            EntityTypeSysName = CmsContentEntityType;
        }
    }
}
