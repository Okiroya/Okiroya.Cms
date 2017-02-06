CREATE PROCEDURE sp_MediaContentGetForFront
@CommonId int,
@SiteId int,
@CurrentDate datetime
AS
BEGIN
	SET NOCOUNT ON;

    declare @SiteGroupId int
	declare @MediaContentEntityTypeId int
	
	set @MediaContentEntityTypeId = 6

	select 
		@SiteGroupId = sites.SiteGroupId
	from dbo.CmsSites as sites
	where sites.SiteId = @SiteId

	declare @tmp table
	(
	MediaId int,
	MediaContentId int,
	TestId int,
	ABGroup nvarchar(50)
	)

	insert into @tmp
	select
		m.MediaId,
		mc.MediaContentId,
		rev.TestId,
		rev.ABGroup
	from dbo.CmsMediaContents as mc
	inner join dbo.CmsMedias as m
		on m.MediaId = mc.MediaId
	inner join dbo.cms_udf_GetLatestCompletedEntityRevisions(@MediaContentEntityTypeId, 0, 1, @CurrentDate) as rev
		on mc.MediaContentId = rev.EntityId
	where 
		mc.CommonId = @CommonId and
		m.SiteGroupId = @SiteGroupId and 
		m.SiteId = @SiteId

	select
		mc.MediaContentId as Id,
		mc.CommonId,
		mc.MediaId,
		mc.ContentName as Title,
		mc.Code,
		mc.CreatedBy,
		mc.CreatedDate,
		mc.ModifiedBy,
		mc.ModifiedDate,
		t.TestId,
		t.ABGroup
	from dbo.CmsMediaContents as mc
	inner join @tmp as t
		on t.MediaContentId = mc.MediaContentId

	select
		mf.MediaFileId as Id,
		t.MediaId,
		mcf.MediaContentId,
		mf.MimeTypeId,
		mf.SiteId,
		mf.SiteGroupId,
		mf.OriginalFileName as 'FileName',
		mf.LinkedFileName,
		mf.FileSize,
		mcf.IsMainFile
	from dbo.CmsMediaContentFiles as mcf
	inner join dbo.CmsMediaFiles as mf
		on mf.MediaFileId = mcf.MediaFileId
	inner join @tmp as t
		on t.MediaContentId = mcf.MediaContentId

	select
		mt.TagId as Id,
		mt.TagName
	from dbo.CmsMediaContentTags as mct
	inner join dbo.CmsMediaTags as mt
		on mt.TagId = mct.MediaTagId
	inner join @tmp as t
		on t.MediaContentId = mct.MediaContentId
END
GO
