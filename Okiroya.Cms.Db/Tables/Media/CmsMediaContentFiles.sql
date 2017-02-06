CREATE TABLE [dbo].[CmsMediaContentFiles](
	[RelationId] [int] IDENTITY(1,1) NOT NULL,
	[MediaContentId] [int] NOT NULL,
	[MediaFileId] [int] NOT NULL,
	[IsMainFile] [bit] NOT NULL,
 CONSTRAINT [PK_CmsMediaContentFiles] PRIMARY KEY CLUSTERED 
(
	[RelationId] ASC
),
CONSTRAINT [FK_CmsMediaContentFiles_CmsMediaContents] FOREIGN KEY([MediaContentId])
REFERENCES [dbo].[CmsMediaContents] ([MediaContentId]),

CONSTRAINT [FK_CmsMediaContentFiles_CmsMediaFiles] FOREIGN KEY([MediaFileId])
REFERENCES [dbo].[CmsMediaFiles] ([MediaFileId])
)
