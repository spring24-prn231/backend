using System;
using System.Collections;
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
        public static ICollection<string> GetNotNullVirtualCollection(this object src)
        {
            var rs = new List<string>();
            var props = src.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.GetMethod.IsVirtual && prop.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    if (prop.PropertyType == typeof(string)) continue;
                    try
                    {
                        var value = src.GetPropertyValue<ICollection>(prop.Name);
                        if (value != null && value.Count > 0)
                        {
                            rs.Add(prop.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
            return rs;
        }
    }
}
