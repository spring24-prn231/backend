using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static T GetPropertyValue<T>(this object src, string propName) 
        {
            return (T)src.GetType().GetProperty(propName)?.GetValue(src, null);
        }
    }
}
