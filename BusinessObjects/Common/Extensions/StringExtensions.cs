using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessObjects.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string o) =>
               Regex.Replace(o, @"(\w)([A-Z])", "$1_$2").ToLower();
    }
}
