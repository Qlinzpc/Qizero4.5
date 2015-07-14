using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CommonExt
    {
        public static string ListToString<T>(this IList<T> list, string property = "ParameterName", string value = "Value")
        {
            var str = new System.Text.StringBuilder();

            PropertyInfo[] oProps = null;
            foreach (T item in list)
            {
                oProps = ((Type)item.GetType()).GetProperties();
                str.AppendFormat(@"     参数名称: {0} , 参数值: {1} ", oProps.First(x => x.Name == property).GetValue(item, null), oProps.First(x => x.Name == value).GetValue(item, null)).AppendLine();
            }

            return str.ToString();
        }
    }
}
