CREATE TABLE [dbo].[CmsEntityMetas](
	[MetaId] [int] IDENTITY(1,1) NOT NULL,
	[MetaLinkId] [int] NOT NULL,
	[EntityId] [int] NOT NULL,
	[DataValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CmsEntityMetas] PRIMARY KEY CLUSTERED 
(
	[MetaId] ASC
),
CONSTRAINT [FK_CmsEntityMetas_CmsEntityMetaLinks] FOREIGN KEY([MetaLinkId])
REFERENCES [dbo].[CmsEntityMetaLinks] ([MetaLinkId])
)
