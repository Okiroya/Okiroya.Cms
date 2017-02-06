using Microsoft.EntityFrameworkCore.Infrastructure;
using Okiroya.Campione.DataAccess;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.DataAccess.Providers;
using System;

namespace Okiroya.Cms.Mvc.DependencyInjection
{
    public class CmsServiceOptions
    {
        public const string CmsScope = "cms";

        public const string CmsAdministrationScope = "cms.administration";

        public int SiteId { get; set; }

        public CmsServiceDataOptions CmsDataOptions { get; set; }

        public CmsServiceDataOptions CmsAdministrationDataOptions { get; set; }

        public CmsServiceOptions()
        {
            CmsDataOptions = new CmsServiceDataOptions();

            CmsAdministrationDataOptions = new CmsServiceDataOptions();
        }
    }

    public class CmsServiceDataOptions
    {
        public string ConnectionString { get; set; }

        public IDataService DataService { get; set; }
    }

    public static class CmsServiceDataOptionsExtension
    {
        public static CmsServiceDataOptions UseMsSql(this CmsServiceDataOptions options)
        {
            Guard.ArgumentNotNull(options);

            options.DataService = new CmsMsSqlDataService(options.ConnectionString);

            return options;
        }

        public static CmsServiceDataOptions UseMsSqlEntityFramework(this CmsServiceDataOptions options, Action<SqlServerDbContextOptionsBuilder> optionsBuilder)
        {
            Guard.ArgumentNotNull(options);
            Guard.ArgumentNotNull(optionsBuilder);

            options.DataService = new CmsMsSqlEntityFrameworkDataService(new CmsMsSqlDbContext(options.ConnectionString, optionsBuilder));

            return options;
        }
    }
}
