CREATE TABLE [dbo].[CmsContentTypes](
	[ContentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[SiteGroupId] [int] NOT NULL,
	[SiteId] [int] NULL,
	[LocaleId] [int] NULL,
	[ContentType] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsEnableMetadata] [bit] NOT NULL,
	[IsSystem] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CmsContentTypes] PRIMARY KEY CLUSTERED 
(
	[ContentTypeId] ASC
),
CONSTRAINT [FK_CmsContentTypes_CmsSiteGroups] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[CmsSiteGroups] ([SiteGroupId]),

CONSTRAINT [FK_CmsContentTypes_CmsSites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[CmsSites] ([SiteId])
)
