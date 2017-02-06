CREATE TABLE [dbo].[CmsMediaContents](
	[MediaContentId] [int] IDENTITY(1,1) NOT NULL,
	[CommonId] [int] NOT NULL,
	[MediaId] [int] NOT NULL,
	[ContentName] [nvarchar](250) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CmsMediaContents] PRIMARY KEY CLUSTERED 
(
	[MediaContentId] ASC
),
CONSTRAINT [FK_CmsMediaContents_CmsMedias] FOREIGN KEY([MediaId])
REFERENCES [dbo].[CmsMedias] ([MediaId]),

CONSTRAINT [FK_CmsMediaContents_Common] FOREIGN KEY([CommonId])
REFERENCES [dbo].[CmsMediaContents] ([MediaContentId])
)
