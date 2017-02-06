CREATE TABLE [dbo].[CmsEntityMetaFields](
	[MetaFieldId] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [nvarchar](250) NOT NULL,
	[FieldDescription] [nvarchar](max) NULL,
	[FieldType] [nvarchar](500) NOT NULL,
	[IsSystem] [bit] NOT NULL,
 CONSTRAINT [PK_CmsEntityMetaFields] PRIMARY KEY CLUSTERED 
(
	[MetaFieldId] ASC
)
)
