CREATE TABLE [dbo].[CmsEntityMetaLinkDefaults](
	[MetaLinkDataId] [int] IDENTITY(1,1) NOT NULL,
	[MetaLinkId] [int] NOT NULL,
	[DataValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CmsEntityMetaLinkDatas] PRIMARY KEY CLUSTERED 
(
	[MetaLinkDataId] ASC
),
CONSTRAINT [FK_CmsEntityMetaLinkDatas_CmsEntityMetaLinks] FOREIGN KEY([MetaLinkId])
REFERENCES [dbo].[CmsEntityMetaLinks] ([MetaLinkId])
)
