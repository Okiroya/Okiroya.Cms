using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain
{
    public class CmsMimeType : Int32EntityObject
    {
        public string Name { get; set; }

        public string ContentType { get; set; }
    }
}
