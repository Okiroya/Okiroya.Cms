using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Domain;
using System;
using System.Collections.Generic;

namespace Okiroya.Cms.BaseWebsite.Domain
{
    public class ProductModel
    {
        public int Id { get; set; }

        public int ProductTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public ProductImageModel[] Images { get; set; }

        public bool HasImages => Images != null;

        public decimal Price { get; set; }

        public string PriceUnit { get; set; }

        public string PriceValue => $"{Price} {PriceUnit}";

        public float Weight { get; set; }

        public string WeightUnit { get; set; }

        public string WeightValue => $"{Weight} {WeightUnit}";

        public ProductModel(CmsContent content)
        {
            Guard.ArgumentNotNull(content);

            Id = content.CommonId;

            ProductTypeId = content.GetMetaValue<int>("ProductType");

            Name = content.Title;

            Description = content.Body;

            Url = content.GetMetaValue<string>("Url");

            Images = content.GetMetaValue(
                "Image",
                (p) =>
                {
                    var result = new List<ProductImageModel>();

                    if (p.ContainsKey("Image[]"))
                    {
                        int count = (int)p["Image[]"];
                        for (int i = 0; i < count; i++)
                        {
                            result.Add(
                                new ProductImageModel
                                {
                                    MediaFileId = (int)p[$"Image[{i}].CmsMediaFile.Id"],
                                    Title = (string)p[$"Image[{i}].CmsMediaFile.LinkedFileName"]
                                });
                        }
                    }

                    return result.ToArray();
                });

            Price = content.GetMetaValue(
                "Price",
                (p)=> 
                {
                    return (decimal)p["Price[0].CmsContent.Body"];
                });
            PriceUnit = content.GetMetaValue(
                "Price",
                (p) =>
                {
                    return (string)p["Price[0].CmsContent.Code"];
                });

            Weight = content.GetMetaValue(
                "Weight",
                (p) =>
                {
                    return (float)p["Weight[0].CmsContent.Body"];
                });
            WeightUnit = content.GetMetaValue(
                "Weight",
                (p) =>
                {
                    return (string)p["Weight[0].CmsContent.Code"];
                });
        }
    }
}
