using Okiroya.Campione.Domain;
using System;

namespace Okiroya.Cms.Domain
{
    public class CmsViewModuleData : Int32EntityObject
    {
        public int ModuleId { get; set; }

        public string DataName { get; set; }

        public string DataType { get; set; }

        public string DataValue { get; set; }
    }
}
