CREATE TABLE [dbo].[CmsMediaTags](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_CmsMediaTags] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
))
