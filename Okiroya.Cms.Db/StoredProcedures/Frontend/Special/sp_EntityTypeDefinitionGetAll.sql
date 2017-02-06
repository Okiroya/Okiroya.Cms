CREATE PROCEDURE  [dbo].[sp_EntityTypeDefinitionGetAll]
AS
BEGIN	
	SET NOCOUNT ON;

    select
	t.EntityTypeId as 'Id',
	t.EntityTypeName as 'Name'
	from dbo.CmsEntityTypeDefinitions as t
END
