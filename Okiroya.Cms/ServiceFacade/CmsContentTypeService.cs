using Okiroya.Campione;
using Okiroya.Campione.Service;
using Okiroya.Campione.SystemUtility.Extensions;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Domain;
using System;
using System.Collections.Generic;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsContentTypeService
    {
        public static CmsContentType[] FindAll(int siteId)
        {
            return EntityServiceFacade<CmsContentType, int>.ExecuteTypedQuery(
                commandName: CmsCommonCommandNames.CmsContentTypeGetAllCommandName,
                parameters: new Dictionary<string, object>().FluentIt(
                    p =>
                    {
                        p.Add("SiteId", siteId);
                    }))
                .DataResult
                .SafeToArray();
        }
    }
}
