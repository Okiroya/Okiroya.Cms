using System;

namespace Okiroya.Cms.Domain
{
    public class CmsRewriteRule : CmsEntity<int>
    {
        public const string CmsRewriteRuleEntityType = "CmsRewriteRule";

        public string OldUrl { get; set; }

        public string NewUrl { get; set; }

        public int StatusCode { get; set; }

        public CmsRewriteRule() : base()
        {
            EntityTypeSysName = CmsRewriteRuleEntityType;
        }
    }
}
