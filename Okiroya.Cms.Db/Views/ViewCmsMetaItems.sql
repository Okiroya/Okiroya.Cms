CREATE VIEW [dbo].[ViewCmsMetaItems]
AS
SELECT        links.EntityTypeId AS LinkEntityTypeId, links.EntityId AS LinkEntityId, metas.EntityId, metas.MetaId, fields.MetaFieldId, fields.FieldName, fields.FieldDescription, fields.FieldType, 
                         links.DisplayLabel AS DisplayName, links.IsRequired, links.IsAllowMultipleValues, links.MetaFieldOrder AS FieldOrder, metas.DataValue AS Data, defaults.DataValue AS Defaults
FROM            dbo.CmsEntityMetaFields AS fields INNER JOIN
                         dbo.CmsEntityMetaLinks AS links ON fields.MetaFieldId = links.MetaFieldId INNER JOIN
                         dbo.CmsEntityMetas AS metas ON links.MetaLinkId = metas.MetaLinkId LEFT OUTER JOIN
                         dbo.CmsEntityMetaLinkDefaults AS defaults ON defaults.MetaLinkId = links.MetaLinkId


