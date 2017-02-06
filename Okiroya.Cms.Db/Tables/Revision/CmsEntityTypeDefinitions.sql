CREATE TABLE [dbo].[CmsEntityTypeDefinitions](
	[EntityTypeId] [int] IDENTITY(1,1) NOT NULL,
	[EntityTypeName] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_CmsEntityTypeDefinitions] PRIMARY KEY CLUSTERED 
(
	[EntityTypeId] ASC
))
