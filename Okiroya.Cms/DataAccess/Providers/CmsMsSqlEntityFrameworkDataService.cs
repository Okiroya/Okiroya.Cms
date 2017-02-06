using Microsoft.EntityFrameworkCore.Infrastructure;
using Okiroya.Campione.DataAccess.EntityFrameworkCore;
using System;

namespace Okiroya.Cms.DataAccess.Providers
{
    public sealed class CmsMsSqlEntityFrameworkDataService : EntityFrameworkDataService<CmsMsSqlDbContext, SqlServerDbContextOptionsBuilder>
    {
        public CmsMsSqlEntityFrameworkDataService(CmsMsSqlDbContext context)
            : base(context)
        { }
    }
}
