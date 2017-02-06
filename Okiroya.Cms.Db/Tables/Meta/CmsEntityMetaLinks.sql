CREATE TABLE [dbo].[CmsEntityMetaLinks](
	[MetaLinkId] [int] IDENTITY(1,1) NOT NULL,
	[MetaFieldId] [int] NOT NULL,
	[EntityTypeId] [int] NOT NULL,
	[EntityId] [int] NOT NULL,
	[DisplayLabel] [nvarchar](150) NOT NULL,
	[IsAllowMultipleValues] [bit] NOT NULL,
	[IsRequired] [bit] NOT NULL,
	[MetaFieldOrder] [int] NOT NULL,
 CONSTRAINT [PK_CmsMetaLinks] PRIMARY KEY CLUSTERED 
(
	[MetaLinkId] ASC
),
CONSTRAINT [FK_CmsEntityMetaLinks_CmsEntityMetaFields] FOREIGN KEY([MetaFieldId])
REFERENCES [dbo].[CmsEntityMetaFields] ([MetaFieldId]),

CONSTRAINT [FK_CmsEntityMetaLinks_CmsEntityTypeDefinitions] FOREIGN KEY([EntityTypeId])
REFERENCES [dbo].[CmsEntityTypeDefinitions] ([EntityTypeId])
)
