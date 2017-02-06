CREATE TABLE [dbo].[CmsPages](
	[PageId] [int] IDENTITY(1,1) NOT NULL,
	[CommonPageId] [int] NOT NULL,
	[ParentPageId] [int] NULL,
	[TemplateId] [int] NOT NULL,
	[SiteId] [int] NULL,
	[SiteGroupId] [int] NOT NULL,
	[LocaleId] [int] NULL,
	[HierarchyLevel] [int] NOT NULL,
	[LevelOrder] [int] NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[Url] [nvarchar](1000) NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[IsHttps] [bit] NOT NULL,
	[CanCached] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CmsPages] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
),
CONSTRAINT [FK_CmsPages_CmsPages] FOREIGN KEY([ParentPageId])
REFERENCES [dbo].[CmsPages] ([PageId]),

CONSTRAINT [FK_CmsPages_CmsSiteGroups] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[CmsSiteGroups] ([SiteGroupId]),

CONSTRAINT [FK_CmsPages_CmsSites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[CmsSites] ([SiteId]),

CONSTRAINT [FK_CmsPages_CmsTemplates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[CmsTemplates] ([TemplateId]),

CONSTRAINT [FK_CmsPages_Common] FOREIGN KEY([CommonPageId])
REFERENCES [dbo].[CmsPages] ([PageId])
)
