CREATE PROCEDURE [dbo].[sp_MenuGetHierarchy]
@SiteId int,
@CurrentDate datetime
AS
BEGIN
	SET NOCOUNT ON;

	declare @SiteGroupId int
	declare @PageEntityTypeId int

	select 
		@SiteGroupId = sites.SiteGroupId
	from dbo.CmsSites as sites
	where sites.SiteId = @SiteId

	set @PageEntityTypeId = 2

	;with h as
	(
	select
		PageId, 
		CommonPageId, 
		ParentPageId,
		SiteGroupId,
		SiteId,
		HierarchyLevel,
		LevelOrder,
		Title,
		Url,
		IsVisible,
		IsHttps 
	from dbo.CmsPages 
		where ParentPageId is null
	union all
	select
		p.PageId, 
		p.CommonPageId, 
		p.ParentPageId,
		p.SiteGroupId,
		p.SiteId,
		p.HierarchyLevel,
		p.LevelOrder,
		p.Title,
		p.Url,
		p.IsVisible,
		p.IsHttps 
	from h as tmp 
		inner join dbo.CmsPages as p 
	on p.ParentPageId = tmp.PageId)

	select 
		h.PageId as 'Id',
		h.CommonPageId,
		h.ParentPageId,
		h.HierarchyLevel,
		h.Title,
		h.Url,
		h.IsHttps,
		rev.TestId,
		rev.ABGroup
	from h
	inner join dbo.cms_udf_GetLatestCompletedEntityRevisions(@PageEntityTypeId, 0, 1, @CurrentDate) as rev
		on h.PageId = rev.EntityId
	where 
		((h.SiteGroupId = @SiteGroupId and h.SiteId is null) or
		(h.SiteGroupId = @SiteGroupId and h.SiteId = @SiteId)) and 
		h.IsVisible = 1
	order by h.ParentPageId, h.LevelOrder

END
