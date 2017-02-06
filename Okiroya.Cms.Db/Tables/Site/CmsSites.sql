CREATE TABLE [dbo].[CmsSites](
	[SiteId] [int] IDENTITY(1,1) NOT NULL,
	[SiteGroupId] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CmsSites] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC
), 
    CONSTRAINT [FK_CmsSites_CmsSiteGroups] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[CmsSiteGroups] ([SiteGroupId])
)


