CREATE TABLE [dbo].[CmsMimeTypes](
	[MimeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[MimeTypeName] [nvarchar](250) NOT NULL,
	[ContentType] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_CmsMimeTypes] PRIMARY KEY CLUSTERED 
(
	[MimeTypeId] ASC
)
)
