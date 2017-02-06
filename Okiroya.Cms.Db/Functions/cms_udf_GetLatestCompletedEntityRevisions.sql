CREATE function [dbo].[cms_udf_GetLatestCompletedEntityRevisions]
(
    @EntityTypeId int,
	@IsDeleted bit,
	@IsPublished bit,
	@CurrentDate datetime
)
returns @revisions table
(
	RevisionId int primary key,
	EntityId int not null,
	TestId int null,
	ABGroup nvarchar(50) null
)
with  EXECUTE AS CALLER
as
begin
	insert into @revisions
	select
		rev.RevisionId,
		rev.EntityId,
		rev.ABTestId,
		test.ABGroup
	from dbo.CmsEntityRevisions as rev
	left join dbo.CmsABTests as test
		on test.TestId = rev.ABTestId
	where 
		rev.EntityTypeId = @EntityTypeId and
		rev.IsDeleted = @IsDeleted and
		rev.IsLatestCompletedRevision = 1 and
		rev.IsPublished = @IsPublished and
		((rev.PublishStartDate is null and rev.PublishEndDate is null) or
		(rev.PublishStartDate is not null and rev.PublishEndDate is null and rev.PublishStartDate <= @CurrentDate) or
		(rev.PublishEndDate is not null and rev.PublishStartDate is null and rev.PublishEndDate > @CurrentDate) or
		(rev.PublishStartDate <= @CurrentDate and rev.PublishEndDate > @CurrentDate))

	return;
end
