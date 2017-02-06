CREATE TABLE [dbo].[CmsABTests](
	[TestId] [int] IDENTITY(1,1) NOT NULL,
	[SiteGroupId] [int] NOT NULL,
	[SiteId] [int] NULL,
	[TestName] [nvarchar](250) NOT NULL,
	[TestDescription] [nvarchar](max) NULL,
	[ABGroup] [nvarchar](50) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_CmsABTests] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC
),
CONSTRAINT [FK_CmsABTests_CmsSiteGroups] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[CmsSiteGroups] ([SiteGroupId]),

CONSTRAINT [FK_CmsABTests_CmsSites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[CmsSites] ([SiteId])
)
