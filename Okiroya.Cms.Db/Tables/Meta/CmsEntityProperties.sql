CREATE TABLE [dbo].[CmsEntityProperties](
	[PropertyId] [int] IDENTITY(1,1) NOT NULL,
	[EntityTypeId] [int] NOT NULL,
	[EntityId] [int] NOT NULL,
	[PropertyName] [nvarchar](250) NOT NULL,
	[PropertyType] [nvarchar](500) NOT NULL,
	[PropertyData] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CmsEntityProperties] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
),
CONSTRAINT [FK_CmsEntityProperties_CmsEntityTypeDefinitions] FOREIGN KEY([EntityTypeId])
REFERENCES [dbo].[CmsEntityTypeDefinitions] ([EntityTypeId])
)
