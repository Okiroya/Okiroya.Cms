CREATE TABLE [dbo].[CmsMedias](
	[MediaId] [int] IDENTITY(1,1) NOT NULL,
	[SiteGroupId] [int] NOT NULL,
	[SiteId] [int] NULL,
	[FolderName] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CmsMedias] PRIMARY KEY CLUSTERED 
(
	[MediaId] ASC
),
CONSTRAINT [FK_CmsMedias_CmsSiteGroups] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[CmsSiteGroups] ([SiteGroupId]),

CONSTRAINT [FK_CmsMedias_CmsSites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[CmsSites] ([SiteId])
)
