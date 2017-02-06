CREATE TABLE [dbo].[CmsTemplates](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[TemplatePath] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_CmsTemplates] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
))
