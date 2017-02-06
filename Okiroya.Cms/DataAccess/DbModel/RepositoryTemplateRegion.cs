using System;

namespace Okiroya.Cms.DataAccess.DbModel
{
    public class RepositoryTemplateRegion
    {
        public string Name { get; set; }

        public RepositoryPageControl[] RegionControls { get; set; }

        public bool HasControls => RegionControls != null;
    }
}
