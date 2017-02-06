CREATE TABLE [dbo].[CmsPageControls](
	[PageControlId] [int] IDENTITY(1,1) NOT NULL,
	[PageId] [int] NOT NULL,
	[TemplateRegionId] [int] NOT NULL,
	[ControlTypeId] [tinyint] NOT NULL,
	[ControlOrder] [int] NOT NULL,
	[ControlContentId] [int] NULL,
	[ControlViewComponentId] [int] NULL,
	[ControlModuleId] [int] NULL,
 CONSTRAINT [PK_CmsPageControls] PRIMARY KEY CLUSTERED 
(
	[PageControlId] ASC
),
CONSTRAINT [FK_CmsPageControls_CmsContents] FOREIGN KEY([ControlContentId])
REFERENCES [dbo].[CmsContents] ([ContentId]),

CONSTRAINT [FK_CmsPageControls_CmsPages] FOREIGN KEY([PageId])
REFERENCES [dbo].[CmsPages] ([PageId]),

CONSTRAINT [FK_CmsPageControls_CmsTemplateRegions] FOREIGN KEY([TemplateRegionId])
REFERENCES [dbo].[CmsTemplateRegions] ([TemplateRegionId]),

CONSTRAINT [FK_CmsPageControls_CmsViewComponents] FOREIGN KEY([ControlViewComponentId])
REFERENCES [dbo].[CmsViewComponents] ([ViewComponentId])
)