CREATE PROCEDURE sp_RewriteRuleGetForFront 
@Url nvarchar(500),
@SiteId int,
@CurrentDate datetime
AS
BEGIN
	SET NOCOUNT ON;

    declare @SiteGroupId int
	declare @RewriteRuleEntityTypeId int

	set @RewriteRuleEntityTypeId = 8

	select 
		@SiteGroupId = sites.SiteGroupId
	from dbo.CmsSites as sites
	where sites.SiteId = @SiteId

	select
		r.RewriteRuleId as Id,
		r.CommonRewriteRuleId as CommonId,
		r.SiteGroupId,
		r.SiteId,
		r.NewUrl,
		r.OldUrl,
		r.StatusCode,
		r.CreatedBy,
		r.CreatedDate,
		r.ModifiedBy,
		r.ModifiedDate,
		rev.TestId,
		rev.ABGroup
	from dbo.CmsRewriteRules as r
	inner join dbo.cms_udf_GetLatestCompletedEntityRevisions(@RewriteRuleEntityTypeId, 0, 1, @CurrentDate) as rev
		on r.RewriteRuleId = rev.EntityId
	where 
		r.OldUrl = @Url and
		r.SiteGroupId = @SiteGroupId and 
		r.SiteId = @SiteId
END
GO
