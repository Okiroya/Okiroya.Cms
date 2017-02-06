CREATE TABLE [dbo].[CmsMediaFiles](
	[MediaFileId] [int] IDENTITY(1,1) NOT NULL,
	[SiteGroupId] [int] NOT NULL,
	[SiteId] [int] NULL,
	[MimeTypeId] [int] NULL,
	[OriginalFileName] [nvarchar](500) NOT NULL,
	[LinkedFileName] [nvarchar](150) NOT NULL,
	[FileSize] [bigint] NOT NULL,
 CONSTRAINT [PK_CmsMediaFiles] PRIMARY KEY CLUSTERED 
(
	[MediaFileId] ASC
),
CONSTRAINT [FK_CmsMediaFiles_CmsMimeTypes] FOREIGN KEY([MimeTypeId])
REFERENCES [dbo].[CmsMimeTypes] ([MimeTypeId]),

CONSTRAINT [FK_CmsMediaFiles_CmsSiteGroups] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[CmsSiteGroups] ([SiteGroupId]),

CONSTRAINT [FK_CmsMediaFiles_CmsSites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[CmsSites] ([SiteId])
)
