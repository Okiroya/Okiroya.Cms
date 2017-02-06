using System;
using System.Collections.Generic;

namespace Okiroya.Cms.ViewModels
{
    public class MenuModel
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public int Level { get; set; }

        public bool IsSelected { get; set; }

        public MenuModel Parent { get; set; }

        public IList<MenuModel> ChildMenu { get; set; }

        public MenuModel()
        {
            ChildMenu = new List<MenuModel>();
        }
    }
}
