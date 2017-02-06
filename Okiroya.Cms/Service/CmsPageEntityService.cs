using Okiroya.Campione.DataAccess;
using Okiroya.Campione.Service;
using Okiroya.Campione.Service.Dynamic;
using Okiroya.Campione.SystemUtility;
using Okiroya.Campione.SystemUtility.Extensions;
using Okiroya.Cms.DataAccess.Commands;
using Okiroya.Cms.Domain;
using Okiroya.Cms.Domain.Property;
using Okiroya.Cms.SystemUtility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okiroya.Cms.Service
{
    public sealed class CmsPageEntityService : BaseEntityService<CmsPage, int>
    {
        protected override IEnumerable<CmsPage> ConvertDataItems(string commandName, IDictionary<string, object> parameters, IEnumerable<DataItem> items)
        {
            Guard.ArgumentNotNull(items);

            if (commandName == CmsCommonCommandNames.CmsPageGetForFrontCommandName)
            {
                CmsPage page = null;

                var firstSelect = items.Where(p => p.Index == 0);

                page = base.ConvertDataItems(commandName, parameters, firstSelect).SafeToItem();

                if (page != null)
                {
                    page.Template = firstSelect
                        .EnumerateDataItems(p => EntityObjectGenerator<int>.CreateEntityObjectFromMeta<CmsTemplate>(p.Items))
                        .SafeToItem();

                    if (page.Template != null)
                    {
                        var secondSelect = items.Where(p => p.Index == 1);

                        page.Template.TemplateRegions = secondSelect
                            .EnumerateDataItems(p => EntityObjectGenerator<int>.CreateEntityObjectFromMeta<CmsTemplateRegion>(p.Items))
                            .SafeToArray();

                        if (page.Template.TemplateRegions != null)
                        {
                            object abGroup = byte.MinValue;
                            if (parameters != null)
                            {
                                parameters.TryGetValue("AbGroup", out abGroup);
                            };

                            var controlContainer = secondSelect
                                .EnumerateDataItems(p => EntityObjectGenerator<int>.CreateEntityObjectFromMeta<CmsPageControl>(p.Items))
                                .SafeToArray();

                            for (int i = 0; i < page.Template.TemplateRegions.Length; i++)
                            {
                                page.Template.TemplateRegions[i].RegionControls = controlContainer
                                    .Where(p => p.TemplateRegionId == page.Template.TemplateRegions[i].Id)
                                    .Where(
                                        (p) =>
                                        {
                                            return p.IsABTest ?
                                                ABGroupHelper.IsInGroup(p.ABGroup, (byte)abGroup) :
                                                true;
                                        })
                                    .SafeToArray();
                            }
                        }
                    }

                    var thirdSelect = items.Where(p => p.Index == 2);

                    page.Properties = thirdSelect
                        .EnumerateDataItems(p => EntityObjectGenerator<int>.CreateEntityObjectFromMeta<CmsPropertyEntityItem>(p.Items))
                        .SafeToArray();
                }

                return new[] { page ?? new CmsPage() };
            }

            return base.ConvertDataItems(commandName, parameters, items);
        }
    }
}
