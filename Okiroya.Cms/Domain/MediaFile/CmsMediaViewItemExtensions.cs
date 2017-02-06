using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.ServiceFacade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okiroya.Cms.Domain.MediaFile
{
    public static class CmsMediaViewItemExtensions
    {
        public static CmsMediaViewItem[] GetMediaViewItems(this CmsContent content, string metaFieldName)
        {
            Guard.ArgumentNotNull(content);
            Guard.ArgumentNotEmpty(metaFieldName);

            return content.GetMetaValue(
                metaFieldName,
                (p) =>
                {
                    var result = new List<CmsMediaViewItem>();

                    int mediaContentId = 0;
                    CmsMediaContent mediaContent = null;
                    if (p.ContainsKey($"{metaFieldName}[]"))
                    {
                        int count = (int)p[$"{metaFieldName}[]"];
                        CmsMediaViewItem item = null;
                        CmsMediaFile file = null;
                        for (int i = 0; i < count; i++)
                        {
                            item = new CmsMediaViewItem
                            {
                                MediaContentId = (int)p[$"{metaFieldName}[{i}].CmsMediaContent.Id"],
                                MediaFileId = (int)p[$"{metaFieldName}[{i}].CmsMediaFile.Id"]
                            };

                            if (item.MediaContentId != mediaContentId)
                            {
                                mediaContentId = item.MediaContentId;

                                mediaContent = CmsMediaContentService.GetMediaContent(mediaContentId, content.SiteId.GetValueOrDefault(), 0); //TODO: передавать текущий abGroupId                                
                            }

                            if (mediaContent != null)
                            {
                                item.Title = mediaContent.Title;

                                file = mediaContent.Files.FirstOrDefault(q => q.Id == item.MediaFileId);
                                if (file != null)
                                {
                                    item.FileName = file.FileName;
                                    item.FileUrl = file.FrontEndUrl;
                                    item.IsMain = file.IsMain;
                                }
                            }

                            result.Add(item);
                        }
                    }

                    return result.ToArray();
                });
        }
    }
}
