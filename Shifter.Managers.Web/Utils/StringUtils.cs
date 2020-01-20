using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shifter.Managers.Web.Utils
{
    public static class StringUtils
    {
        public static IEnumerable<int> ConvertToList(string values)
        {
            var valueList = new List<int>();
            if (values.IsNotNullOrEmpty())
            {
                valueList = values.Split(',').Select(val => int.Parse(val)).ToList();
            }

            return valueList;
        }
    }
}