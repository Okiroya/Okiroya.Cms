using System;

namespace Okiroya.Cms.DataAccess.DbModel
{
    public class RepositoryTemplate
    {
        public string Name { get; set; }

        public string TemplatePath { get; set; }

        public RepositoryTemplateRegion[] TemplateRegions { get; set; }

        public int? TemplateRegionsCount => TemplateRegions?.Length;        

        public bool HasRegions => TemplateRegions != null;
    }
}
