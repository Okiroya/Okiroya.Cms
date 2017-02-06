using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace Okiroya.Cms.Mvc.UI
{
    public static class ModelHelper
    {
        public static dynamic ToDynamic(this object obj)
        {
            var result = new ExpandoObject();

            if (obj != null)
            {
                var dictionary = result as IDictionary<string, object>;

                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
                {
                    dictionary.Add(descriptor.Name, descriptor.GetValue(obj));
                }
            }

            return result;
        }
    }
}
