CREATE TABLE [dbo].[CmsTemplateRegions](
	[TemplateRegionId] [int] IDENTITY(1,1) NOT NULL,
	[TemplateId] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_CmsTemplateRegions] PRIMARY KEY CLUSTERED 
(
	[TemplateRegionId] ASC
), 
    CONSTRAINT [FK_CmsTemplateRegions_CmsTemplates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[CmsTemplates] ([TemplateId])
)