CREATE TABLE [dbo].[CmsMediaContentTags](
	[RelationId] [int] IDENTITY(1,1) NOT NULL,
	[MediaContentId] [int] NOT NULL,
	[MediaTagId] [int] NOT NULL,
 CONSTRAINT [PK_CmsMediaContentTags] PRIMARY KEY CLUSTERED 
(
	[RelationId] ASC
),
CONSTRAINT [FK_CmsMediaContentTags_CmsMediaContents] FOREIGN KEY([MediaContentId])
REFERENCES [dbo].[CmsMediaContents] ([MediaContentId]),

CONSTRAINT [FK_CmsMediaContentTags_CmsMediaTags] FOREIGN KEY([MediaTagId])
REFERENCES [dbo].[CmsMediaTags] ([TagId])
)
