namespace Qz.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public static class StringExtensions
    {
        public static string QFormat(this string str, params object[] args)
        {
            if (str.IsNullOrEmpty())
            {
                return "";
            }

            return string.Format(str, args);
        }

        public static string QFormat(this string str, object arg0)
        {
            if (str.IsNullOrEmpty())
            {
                return "";
            }

            return string.Format(str, arg0);
        }

        public static string QFormat(this string str, object arg0, object arg1)
        {
            if (str.IsNullOrEmpty())
            {
                return "";
            }

            return string.Format(str, arg0, arg1);
        }

        public static string QFormat(this string str, object arg0, object arg1, object arg2)
        {
            if (str.IsNullOrEmpty())
            {
                return "";
            }

            return string.Format(str, arg0, arg1, arg2);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
