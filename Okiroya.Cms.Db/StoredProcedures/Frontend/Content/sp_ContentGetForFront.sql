CREATE PROCEDURE [dbo].[sp_ContentGetForFront]
@CommonContentId int,
@SiteId int,
@CurrentDate datetime
AS
BEGIN
	declare @SiteGroupId int
	declare @ContentTypeId int
	declare @ContentEntityTypeId int
	declare @ContentTypeEntityTypeId int

	declare @tmp table
	(
	ContentTypeId int,
	ContentId int,
	TestId int,
	ABGroup nvarchar(50)
	)

	set @ContentEntityTypeId = 4
	set @ContentTypeEntityTypeId = 7

	select 
		@SiteGroupId = sites.SiteGroupId
	from dbo.CmsSites as sites
	where sites.SiteId = @SiteId

	insert into @tmp
	select
		c.ContentTypeId,
		c.ContentId,
		rev.TestId,
		rev.ABGroup
	from dbo.CmsContents as c
	inner join dbo.cms_udf_GetLatestCompletedEntityRevisions(@ContentEntityTypeId, 0, 1, @CurrentDate) as rev
		on c.ContentId = rev.EntityId
	where 
		c.CommonContentId = @CommonContentId and
		c.SiteGroupId = @SiteGroupId and 
		c.SiteId = @SiteId

	select 
		c.ContentId as 'Id',
		c.CommonContentId as 'CommonId',
		c.ContentTypeId,
		c.SiteGroupId,
		c.SiteId,
		t.TestId,
		t.ABGroup,
		c.Title,
		c.Body,
		c.Code,
		c.CreatedBy,
		c.CreatedDate,
		c.ModifiedBy,
		c.ModifiedDate
	from dbo.CmsContents as c
	inner join @tmp as t
		on t.ContentId = c.ContentId

	select
		metas.LinkEntityId as 'Id',
		metas.EntityId,
		metas.FieldName,
		metas.FieldType,
		metas.IsRequired,
		metas.IsAllowMultipleValues,
		metas.FieldOrder,
		metas.Data,
		metas.Defaults
	from dbo.ViewCmsMetaItems as metas
	inner join @tmp as t
		on t.ContentTypeId = metas.LinkEntityId
	where metas.LinkEntityTypeId = @ContentTypeEntityTypeId
			
END
