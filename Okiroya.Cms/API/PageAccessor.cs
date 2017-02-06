using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Domain;
using Okiroya.Cms.ServiceFacade;
using Okiroya.Cms.SystemUtility;
using System;

namespace Okiroya.Cms.API
{
    public class PageAccessor : IHideObjectMembersForFluentApi
    {
        private ICmsContext _context;

        public PageAccessor(ICmsContext context)
        {
            Guard.ArgumentNotNull(context);

            _context = context;
        }

        public CmsPage Current
        {
            get
            {
                return _context.CurrentPage;
            }
        }

        public CmsPage[] All
        {
            get
            {
                return CmsPageService.GetAllCmsPages(_context.SiteId);
            }
        }
    }
}
