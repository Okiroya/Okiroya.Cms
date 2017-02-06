CREATE TABLE [dbo].[CmsViewComponentDatas](
	[ViewComponentDataId] [int] IDENTITY(1,1) NOT NULL,
	[ViewComponentId] [int] NOT NULL,
	[PageControlId] [int] NOT NULL,
	[DataName] [nvarchar](250) NOT NULL,
	[DataType] [nvarchar](150) NOT NULL,
	[DataValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CmsViewComponentDatas] PRIMARY KEY CLUSTERED 
(
	[ViewComponentDataId] ASC
),
CONSTRAINT [FK_CmsViewComponentDatas_CmsPageControls] FOREIGN KEY([PageControlId])
REFERENCES [dbo].[CmsPageControls] ([PageControlId]),

CONSTRAINT [FK_CmsViewComponentDatas_CmsViewComponents] FOREIGN KEY([ViewComponentId])
REFERENCES [dbo].[CmsViewComponents] ([ViewComponentId])
)
