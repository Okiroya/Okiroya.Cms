CREATE TABLE [dbo].[CmsContents](
	[ContentId] [int] IDENTITY(1,1) NOT NULL,
	[CommonContentId] [int] NOT NULL,
	[ContentTypeId] [int] NOT NULL,
	[SiteGroupId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[LocaleId] [int] NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CmsContents] PRIMARY KEY CLUSTERED 
(
	[ContentId] ASC
),
CONSTRAINT [FK_CmsContents_CmsContentTypes] FOREIGN KEY([ContentTypeId])
REFERENCES [dbo].[CmsContentTypes] ([ContentTypeId]),

CONSTRAINT [FK_CmsContents_CmsSiteGroups] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[CmsSiteGroups] ([SiteGroupId]),

CONSTRAINT [FK_CmsContents_CmsSites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[CmsSites] ([SiteId]),

CONSTRAINT [FK_CmsContents_Common] FOREIGN KEY([CommonContentId])
REFERENCES [dbo].[CmsContents] ([ContentId])
)
