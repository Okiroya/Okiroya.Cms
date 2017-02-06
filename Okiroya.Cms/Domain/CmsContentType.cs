using Okiroya.Cms.Domain.Meta;
using System;

namespace Okiroya.Cms.Domain
{
    public sealed class CmsContentType : CmsMetaEntity<int>
    {
        public const string CmsContentTypeEntityType = "CmsContentType";

        public string ContentType { get; set; }

        public bool IsEnabledMetadata { get; set; }

        public CmsContentType()
            : base()
        {
            EntityTypeSysName = CmsContentTypeEntityType;
        }
    }
}
