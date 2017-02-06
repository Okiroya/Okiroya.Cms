using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain.Meta
{
    public class CmsMetaEntityItem : Int32EntityObject
    {
        public int EntityId { get; set; }
        
        public string FieldName { get; set; }

        public string FieldType { get; set; }

        public string DisplayName { get; set; }

        public bool IsAllowMultipleValues { get; set; }

        public bool IsRequired { get; set; }

        public int Order { get; set; }

        public string Data { get; set; }

        public string Defaults { get; set; }
    }
}
