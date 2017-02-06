using Okiroya.Campione.DataAccess;
using Okiroya.Campione.DataAccess.MsSql;
using Okiroya.Campione.SystemUtility.DI;
using Okiroya.Cms.DataAccess.Mappers;
using Okiroya.Cms.Domain;
using System;

namespace Okiroya.Cms.DataAccess.Providers
{
    public sealed class CmsMsSqlDataService : DirectMsSqlDataService
    {
        static CmsMsSqlDataService()
        {
            RegisterDependencyContainer<IDataServiceMapper>.RegisterFor(typeof(CmsTemplate), new CmsTemplateDataServiceMapper());
            RegisterDependencyContainer<IDataServiceMapper>.RegisterFor(typeof(CmsPageControl), new CmsPageControDataServiceMapper());
        }

        public CmsMsSqlDataService(string connectionName)
            : base(connectionName)
        { }
    }
}
