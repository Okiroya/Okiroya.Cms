using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain
{
    public sealed class CmsMediaTag : Int32EntityObject
    {
        public string TagName { get; set; }
    }
}
