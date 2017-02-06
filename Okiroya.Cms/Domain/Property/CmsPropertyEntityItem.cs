using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain.Property
{
    public class CmsPropertyEntityItem : Int32EntityObject
    {
        public int EntityId { get; set; }

        public string PropertyName { get; set; }

        public string PropertyType { get; set; }

        public string PropertyData { get; set; }
    }
}
