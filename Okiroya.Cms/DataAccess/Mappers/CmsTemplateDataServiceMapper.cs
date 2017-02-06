using Okiroya.Campione.DataAccess;
using System;

namespace Okiroya.Cms.DataAccess.Mappers
{
    public sealed class CmsTemplateDataServiceMapper : IDataServiceMapper
    {
        public string MapFromDomainNameToRepositoryOne(string sourceName)
        {
            string result = sourceName;
            switch (sourceName)
            {
                case "Id":
                    result = "TemplateId";
                    break;
                case "Name":
                    result = "TemplateName";
                    break;
            }

            return result;
        }
    }
}
