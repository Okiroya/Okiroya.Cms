using Okiroya.Campione.DataAccess;
using System;

namespace Okiroya.Cms.DataAccess.Mappers
{
    public sealed class CmsPageControDataServiceMapper : IDataServiceMapper
    {
        public string MapFromDomainNameToRepositoryOne(string sourceName)
        {
            string result = sourceName;
            switch (sourceName)
            {
                case "Id":
                    result = "PageControl";
                    break;
            }

            return result;
        }
    }
}
