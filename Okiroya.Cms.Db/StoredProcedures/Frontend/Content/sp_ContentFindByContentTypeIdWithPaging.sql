CREATE PROCEDURE [dbo].[sp_ContentFindByContentTypeIdWithPaging]
@ContentTypeId int,
@SiteId int,
@CurrentDate datetime,
@Page int,
@PageSize int,
@Total int out
AS
BEGIN
	SET NOCOUNT ON;

	declare @ContentEntityTypeId int
	declare @ContentTypeEntityTypeId int
	declare @SiteGroupId int

	set @ContentEntityTypeId = 4
	set @ContentTypeEntityTypeId = 7
    
	select 
		@SiteGroupId = sites.SiteGroupId
	from dbo.CmsSites as sites
	where sites.SiteId = @SiteId
	
	declare @first int
	declare @last int

	set @first = (@Page - 1) * @PageSize + 1
	set @last = @first + @PageSize - 1

	declare @tmp table
	(
	RowNumber int,
	ContentTypeId int,
	ContentId int,
	TestId int,
	ABGroup nvarchar(50)
	)

	insert into @tmp	
	select		
		row_number() over (order by c.ContentId) as RowNumber,
		c.ContentTypeId,
		c.ContentId,
		rev.TestId,
		rev.ABGroup
	from dbo.CmsContents as c
	inner join dbo.cms_udf_GetLatestCompletedEntityRevisions(@ContentEntityTypeId, 0, 1, @CurrentDate) as rev
		on c.ContentId = rev.EntityId
	where 
		c.ContentTypeId = @ContentTypeId and
		c.SiteGroupId = @SiteGroupId and 
		c.SiteId = @SiteId
	
	select 
		@Total = count(tmp.RowNumber) 
	from @tmp as tmp
	
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
	where t.RowNumber between @first and @last

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
		on t.ContentTypeId = metas.LinkEntityId and
		t.ContentId = metas.EntityId
	where metas.LinkEntityTypeId = @ContentTypeEntityTypeId
		and t.RowNumber between @first and @last
END
