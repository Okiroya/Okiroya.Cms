CREATE PROCEDURE [dbo].[sp_PageInfoFind]
@SiteId int,
@CurrentDate datetime
AS
BEGIN
	declare @SiteGroupId int
	declare @PageEntityTypeId int

	set @PageEntityTypeId = 2

	select 
		@SiteGroupId = sites.SiteGroupId
	from dbo.CmsSites as sites
	where sites.SiteId = @SiteId

	select 
		p.PageId as 'Id',
		p.CommonPageId as 'CommonId',
		rev.TestId,
		rev.ABGroup,
		p.Url,
		p.CanCached,
		p.SiteGroupId,
		p.SiteId,
		templates.TemplatePath
	from dbo.CmsPages as p
	inner join dbo.cms_udf_GetLatestCompletedEntityRevisions(@PageEntityTypeId, 0, 1, @CurrentDate) as rev
		on p.PageId = rev.EntityId	
	inner join dbo.CmsTemplates as templates
		on templates.TemplateId = p.TemplateId
	where 
		((p.SiteGroupId = @SiteGroupId and p.SiteId is null) or
		(p.SiteGroupId = @SiteGroupId and p.SiteId = @SiteId))

END
