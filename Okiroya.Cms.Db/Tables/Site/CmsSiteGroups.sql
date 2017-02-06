CREATE TABLE [dbo].[CmsSiteGroups](
	[SiteGroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_SiteGroups] PRIMARY KEY CLUSTERED 
(
	[SiteGroupId] ASC
))
