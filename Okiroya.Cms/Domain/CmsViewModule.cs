using Okiroya.Cms.Service;
using Okiroya.Cms.ServiceFacade;
using System;

namespace Okiroya.Cms.Domain
{
    public class CmsViewModule : CmsEntity<int>
    {
        public const string CmsViewComponentEntityType = "CmsViewComponent";

        private CmsViewModuleData[] _rawData;
        private bool _isDataPopulated;
        private dynamic _data;

        public string Title { get; set; }

        public string ComponentPath { get; set; }

        public string Description { get; set; }

        public CmsViewModuleData[] RawData
        {
            get
            {
                if (_rawData == null)
                {
                    _rawData = CmsViewModuleService.FindCmsViewModuleData(Id);

                    _isDataPopulated = false;
                }

                return _rawData;
            }
            set
            {
                _rawData = value;

                _isDataPopulated = false;
            }
        }

        public dynamic Data
        {
            get
            {
                if (!_isDataPopulated)
                {
                    _data = MetaPropertyProvider.ConvertDataToExpando(this);

                    _isDataPopulated = true;
                }

                return _data;
            }
        }

        public CmsViewModule()
            : base()
        {
            EntityTypeSysName = CmsViewComponentEntityType;
        }
    }
}
