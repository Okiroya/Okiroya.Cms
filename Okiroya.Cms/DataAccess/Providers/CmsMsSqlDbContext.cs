using Okiroya.Campione.DataAccess.EntityFrameworkCore.Providers.MsSql;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Okiroya.Cms.DataAccess.Providers
{
    public sealed class CmsMsSqlDbContext : MsSqlDbContext
    {
        public CmsMsSqlDbContext(string connectionString, Action<SqlServerDbContextOptionsBuilder> optionsBuilderConfiguration)
            : base(connectionString, optionsBuilderConfiguration)
        { }
    }
}
