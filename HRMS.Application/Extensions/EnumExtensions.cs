using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
        public static string ToDescriptionString(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString())!.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes?.Length > 0
                ? attributes[0].Description
                : val.ToString();
        }
    }
}
