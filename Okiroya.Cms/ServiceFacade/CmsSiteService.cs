using Okiroya.Campione;
using Okiroya.Campione.Service;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Domain;
using System;
using System.Collections.Generic;

namespace Okiroya.Cms.ServiceFacade
{
    public static class CmsSiteService
    {
        public static CmsSiteGroup GetSiteGroup(int siteGroupId)
        {
            return EntityServiceFacade<CmsSiteGroup, int>.GetItem(
                commandName: CmsCommonCommandNames.CmsSiteGroupGetCommandName,
                parameters: new Dictionary<string, object>()
                    .FluentIt(
                        p =>
                        {
                            p.Add("SiteGroupId", siteGroupId);
                        }));
        }
    }
}
