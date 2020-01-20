using System;
using System.ComponentModel;
using System.Linq;

namespace Framework.Extensions
{
    public static class EnumExtentions
    {
        /// <summary>
        /// Gets the description form the enum, if one is not available then returns the enum string value
        /// </summary>
        public static string Description(this Enum value)
        {
            var attribute = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
