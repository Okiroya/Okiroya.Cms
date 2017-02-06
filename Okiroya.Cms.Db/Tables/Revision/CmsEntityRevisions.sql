CREATE TABLE [dbo].[CmsEntityRevisions](
	[RevisionId] [bigint] IDENTITY(1,1) NOT NULL,
	[EntityId] [int] NOT NULL,
	[EntityTypeId] [int] NOT NULL,
	[WorkflowId] [uniqueidentifier] NULL,
	[ABTestId] [int] NULL,
	[IsLatestRevision] [bit] NOT NULL,
	[IsLatestCompletedRevision] [bit] NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[PublishStartDate] [datetime] NULL,
	[PublishEndDate] [datetime] NULL,
 CONSTRAINT [PK_CmsEntityRevisions] PRIMARY KEY CLUSTERED 
(
	[RevisionId] ASC
),
CONSTRAINT [FK_CmsEntityRevisions_CmsABTests] FOREIGN KEY([ABTestId])
REFERENCES [dbo].[CmsABTests] ([TestId]),

CONSTRAINT [FK_CmsEntityRevisions_CmsEntityTypeDefinitions] FOREIGN KEY([EntityTypeId])
REFERENCES [dbo].[CmsEntityTypeDefinitions] ([EntityTypeId])
)
