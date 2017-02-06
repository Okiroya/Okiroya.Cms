using Okiroya.Campione.Service.Paging;
using Okiroya.Campione.SystemUtility;
using Okiroya.Campione.SystemUtility.DI;
using Okiroya.Campione.SystemUtility.Extensions;
using Okiroya.Cms.Domain;
using Okiroya.Cms.ServiceFacade;
using Okiroya.Cms.SystemUtility;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Okiroya.Cms.API
{
    public abstract class ContentAccessorBase<T> : IHideObjectMembersForFluentApi
    {
        internal ICmsContext Context { get; private set; }

        public ContentAccessorBase(ICmsContext context)
        {
            Guard.ArgumentNotNull(context);

            Context = context;
        }

        public T Get(int id)
        {
            return Convert(CmsContentService.GetCmsContent(id, Context.SiteId, Context.CurrentABGroup));
        }

        public Task<T> GetAsync(int id)
        {
            return GetAsync(id, CancellationToken.None);
        }

        public async Task<T> GetAsync(int id, CancellationToken token)
        {
            return Convert(await CmsContentService.GetCmsContentAsync(id, Context.SiteId, Context.CurrentABGroup, token).ConfigureAwait(false));
        }

        public T[] FindForContentType(int id)
        {
            var data = CmsContentService.FindCmsContentForContentType(id, Context.SiteId, Context.CurrentABGroup);

            return data
                .Select(p => Convert(p))
                .SafeToArray();
        }

        public Task<T[]> FindForContentTypeAsync(int id)
        {
            return FindForContentTypeAsync(id, CancellationToken.None);
        }

        public async Task<T[]> FindForContentTypeAsync(int id, CancellationToken token)
        {
            var data = await CmsContentService.FindCmsContentForContentTypeAsync(id, Context.SiteId, Context.CurrentABGroup, token);

            return data
                .Select(p => Convert(p))
                .SafeToArray();
        }

        public PagedCollection<T> FindForContentTypePaged(int id, int page = 1, int pageSize = 0)
        {
            var data = CmsContentService.FindCmsContentForContentTypePaged(id, Context.SiteId, Context.CurrentABGroup, page, pageSize);

            return new PagedCollection<T>(data.Select(p => Convert(p)))
            {
                PageIndex = data.PageIndex,
                PageSize = data.PageSize,
                TotalCount = data.TotalCount,
                SortBy = data.SortBy,
                IsAscendingSort = data.IsAscendingSort,
                InParams = data.InParams,
                OutParams = data.OutParams
            };
        }

        public Task<PagedCollection<T>> FindForContentTypePagedAsync(int id)
        {
            return FindForContentTypePagedAsync(id, CancellationToken.None);
        }

        public async Task<PagedCollection<T>> FindForContentTypePagedAsync(int id, CancellationToken token, int page = 1, int pageSize = 0)
        {
            var data = await CmsContentService
                .FindCmsContentForContentTypePagedAsync(id, Context.SiteId, Context.CurrentABGroup, token, page, pageSize)
                .ConfigureAwait(false);

            return new PagedCollection<T>(data.Select(p => Convert(p)))
            {
                PageIndex = data.PageIndex,
                PageSize = data.PageSize,
                TotalCount = data.TotalCount,
                SortBy = data.SortBy,
                IsAscendingSort = data.IsAscendingSort,
                InParams = data.InParams,
                OutParams = data.OutParams
            };
        }

        protected abstract T Convert(CmsContent content);
    }

    public class ContentAccessor : ContentAccessorBase<CmsContent>
    {
        public ContentAccessor(ICmsContext context)
            : base(context)
        { }

        public ContentAccessor<T> As<T>(IContentMapper<T> mapper = null) where T : class
        {
            return new ContentAccessor<T>(Context, mapper);
        }

        protected override CmsContent Convert(CmsContent content)
        {
            return content;
        }
    }

    public class ContentAccessor<T> : ContentAccessorBase<T> where T : class
    {
        private IContentMapper<T> _mapper;

        public ContentAccessor(ICmsContext context, IContentMapper<T> mapper = null)
            : base(context)
        {
            if (mapper == null)
            {
                _mapper = RegisterDependencyContainer<IContentMapper<T>>.Resolve();
            }
            else
            {
                _mapper = mapper;
            }
        }

        protected override T Convert(CmsContent content)
        {
            return _mapper.MapFromContent(content);
        }
    }
}
