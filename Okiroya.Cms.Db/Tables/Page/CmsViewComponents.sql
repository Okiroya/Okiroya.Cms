CREATE TABLE [dbo].[CmsViewComponents](
	[ViewComponentId] [int] IDENTITY(1,1) NOT NULL,
	[CommonId] [int] NOT NULL,
	[ComponentPath] [nvarchar](500) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CmsViewComponents] PRIMARY KEY CLUSTERED 
(
	[ViewComponentId] ASC
),
CONSTRAINT [FK_CmsViewComponents_Common] FOREIGN KEY([CommonId])
REFERENCES [dbo].[CmsViewComponents] ([ViewComponentId])
)
