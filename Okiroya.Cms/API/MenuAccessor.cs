using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Domain;
using Okiroya.Cms.ServiceFacade;
using Okiroya.Cms.SystemUtility;
using Okiroya.Cms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Okiroya.Cms.API
{
    public class MenuAccessor : IHideObjectMembersForFluentApi
    {
        private ICmsContext _context;

        public MenuAccessor(ICmsContext context)
        {
            Guard.ArgumentNotNull(context);

            _context = context;
        }

        public MenuModel MenuItems(int startLevel, int maxLevel = -1)
        {
            var hierarchy = CmsMenuService.GetMenuHierarchy(_context.SiteId, _context.CurrentABGroup);

            return ConstructHierarchy(hierarchy);
        }

        public async Task<MenuModel> MenuItemsAsync(int startLevel, int maxLevel = -1)
        {
            var hierarchy = await CmsMenuService
                .GetMenuHierarchyAsync(_context.SiteId, _context.CurrentABGroup, CancellationToken.None)
                .ConfigureAwait(false);

            return ConstructHierarchy(hierarchy);
        }

        public MenuModel Breadcrumbs()
        {
            var hierarchy = CmsMenuService.GetMenuHierarchy(_context.SiteId, _context.CurrentABGroup);

            return ConstructHierarchy(hierarchy);
        }

        public async Task<MenuModel> BreadcrumbsAsync()
        {
            var hierarchy = await CmsMenuService
                .GetMenuHierarchyAsync(_context.SiteId, _context.CurrentABGroup, CancellationToken.None)
                .ConfigureAwait(false);

            return ConstructHierarchy(hierarchy);
        }

        public MenuModel Sitemap()
        {
            var hierarchy = CmsMenuService.GetMenuHierarchy(_context.SiteId, _context.CurrentABGroup);

            return ConstructHierarchy(hierarchy);
        }

        public async Task<MenuModel> SitemapAsync()
        {
            var hierarchy = await CmsMenuService
                .GetMenuHierarchyAsync(_context.SiteId, _context.CurrentABGroup, CancellationToken.None)
                .ConfigureAwait(false);

            return ConstructHierarchy(hierarchy);
        }

        private MenuModel ConstructHierarchy(CmsMenuItem[] items)
        {
            MenuModel result = null;

            if (items.Length > 0)
            {
                var map = new Dictionary<int, MenuModel>();
                for (int i = 0; i < items.Length; i++)
                {
                    if (!items[i].ParentPageId.HasValue)
                    {
                        map.Add(
                            items[i].Id,
                            new MenuModel
                            {
                                Title = items[i].Title,
                                Url = items[i].Url,
                                Level = items[i].HierarchyLevel,
                                IsSelected = true
                            });
                    }
                    else
                    {
                        if (map.ContainsKey(items[i].ParentPageId.Value))
                        {
                            map[items[i].ParentPageId.Value].ChildMenu.Add(
                                new MenuModel
                                {
                                    Title = items[i].Title,
                                    Url = items[i].Url,
                                    Level = items[i].HierarchyLevel,
                                    Parent = map[items[i].ParentPageId.Value]
                                });
                        }
                        else
                        {
                            map.Add(
                                items[i].ParentPageId.Value,
                                new MenuModel
                                {
                                    Title = items[i].Title,
                                    Url = items[i].Url,
                                    Level = items[i].HierarchyLevel
                                });
                        }
                    }
                }

                result = map.FirstOrDefault().Value;
            }

            return result ?? new MenuModel();
        }
    }
}
