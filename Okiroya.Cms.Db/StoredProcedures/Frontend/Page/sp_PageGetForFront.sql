CREATE PROCEDURE [dbo].[sp_PageGetForFront]
@CommonPageId int,
@SiteGroupId int,
@SiteId int = null,
@CurrentDate datetime,
@AbGroup tinyint
AS
BEGIN
	declare @PageId int
	declare @TemplateId int
	declare @PageEntityTypeId int
	declare @PageControlEntityTypeId int

	set @PageEntityTypeId = 2
	set @PageControlEntityTypeId = 3

	select 
		@PageId = p.PageId,
		@TemplateId = p.TemplateId
	from dbo.CmsPages as p
	inner join dbo.cms_udf_GetLatestCompletedEntityRevisions(@PageEntityTypeId, 0, 1, @CurrentDate) as rev
		on p.PageId = rev.EntityId
	where 
		p.CommonPageId = @CommonPageId and
		((p.SiteGroupId = @SiteGroupId and p.SiteId is null) or
		(p.SiteGroupId = @SiteGroupId and p.SiteId = @SiteId))

	select 		
		p.PageId as 'Id',
		p.CommonPageId as 'CommonId',
		p.ParentPageId,
		p.TemplateId,
		p.SiteId,
		p.SiteGroupId,
		p.Title as 'Name',
		p.Url,
		p.IsVisible,
		p.CanCached,
		p.CreatedBy,
		p.CreatedDate,
		p.ModifiedBy,
		p.ModifiedDate,
		t.Name as 'TemplateName',
		t.TemplatePath
	from dbo.CmsPages as p
	inner join dbo.CmsTemplates as t
		on t.TemplateId = p.TemplateId
	where p.PageId = @PageId
		
	select
		tr.TemplateRegionId as 'Id',
		tr.Name,
		@TemplateId as TemplateId,
		ipc.PageControlId,
		ipc.TestId,
		ipc.ABGroup,
		ipc.TemplateRegionId,
		ipc.ControlTypeId,
		ipc.ControlContentId,
		ipc.ControlViewComponentId,
		ipc.ControlModuleId
	from dbo.CmsTemplateRegions as tr
	left join 
		(select		
			pc.PageControlId,
			rev.TestId,
			rev.ABGroup,
			pc.PageId,
			pc.TemplateRegionId,
			pc.ControlTypeId,
			pc.ControlOrder,
			pc.ControlContentId,
			pc.ControlViewComponentId,
			pc.ControlModuleId
		from dbo.CmsPageControls as pc
		inner join dbo.cms_udf_GetLatestCompletedEntityRevisions(@PageControlEntityTypeId, 0, 1, @CurrentDate) as rev
			on pc.PageControlId = rev.EntityId
		) as ipc
	on ipc.PageId = @PageId and ipc.TemplateRegionId = tr.TemplateRegionId
	where 
		tr.TemplateId = @TemplateId 
	order by ipc.ControlOrder

	select
		pr.PropertyId as 'Id',
		pr.EntityId,
		pr.PropertyName,
		pr.PropertyType,
		pr.PropertyData
	from dbo.CmsEntityProperties as pr
	where pr.EntityId = @PageId and pr.EntityTypeId = @PageEntityTypeId

END
