CREATE PROCEDURE [dbo].[sp_ViewComponentGetForFront]
@CommonId int,
@PageControlId int,
@WithModel bit,
@CurrentDate datetime
AS
BEGIN	
	SET NOCOUNT ON;

	declare @ViewComponentEntityTypeId int

	declare @vc table
	(
		Id int primary key not null,
		TestId int null,
		ABGroup nvarchar(50)
	) 

	set @ViewComponentEntityTypeId = 5

	insert into @vc
	select 
		vc.ViewComponentId,
		rev.TestId,
		rev.ABGroup		
	from dbo.CmsViewComponents as vc
	inner join dbo.cms_udf_GetLatestCompletedEntityRevisions(@ViewComponentEntityTypeId, 0, 1, @CurrentDate) as rev
		on vc.ViewComponentId = rev.EntityId
	where
		vc.CommonId = @CommonId

    select 
		vc.ViewComponentId as 'Id',
		vc.CommonId,
		tmp.TestId,
		tmp.ABGroup,
		vc.ComponentPath,
		vc.Title,
		vc.Description,
		vc.CreatedBy,
		vc.CreatedDate,
		vc.ModifiedBy,
		vc.ModifiedDate
	from dbo.CmsViewComponents as vc
	inner join @vc as tmp
		on vc.ViewComponentId = tmp.Id

	if @WithModel = 1
	begin
		select
			vcd.ViewComponentDataId as 'Id',
			vcd.ViewComponentId as 'ModuleId',
			vcd.DataName,
			vcd.DataType,
			vcd.DataValue
		from dbo.CmsViewComponentDatas as vcd
		inner join @vc as tmp
			on vcd.ViewComponentId = tmp.Id and
			vcd.PageControlId = @PageControlId
	end
END
