CREATE TABLE [dbo].[CmsRewriteRules](
	[RewriteRuleId] [int] IDENTITY(1,1) NOT NULL,
	[CommonRewriteRuleId] [int] NOT NULL,
	[SiteGroupId] [int] NOT NULL,
	[SiteId] [int] NULL,
	[OldUrl] [nvarchar](500) NOT NULL,
	[NewUrl] [nvarchar](500) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CmsRewriteRules] PRIMARY KEY CLUSTERED 
(
	[RewriteRuleId] ASC
),
CONSTRAINT [FK_CmsRewriteRules_CmsSiteGroups] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[CmsSiteGroups] ([SiteGroupId]),

CONSTRAINT [FK_CmsRewriteRules_CmsSites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[CmsSites] ([SiteId]),

CONSTRAINT [FK_CmsRewriteRules_Common] FOREIGN KEY([CommonRewriteRuleId])
REFERENCES [dbo].[CmsRewriteRules] ([RewriteRuleId])
)
