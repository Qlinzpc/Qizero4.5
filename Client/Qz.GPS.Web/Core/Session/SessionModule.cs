
namespace Qz.GPS.Web.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Qz.GPS.ViewModel;
    using Qz.Common;

    public class SessionModule
    {
        public static List<Module> ListData
        {
            get
            {
                var list = HttpContext.Current.Session[QConst.SESSION_MODULE_LIST_NAME] as List<Module>;

                list = list ?? new List<Module>();

                return list;
            }
        }

        public static string JsonData
        {
            get
            {
                var json = HttpContext.Current.Session[QConst.SESSION_MODULE_JSON_NAME] as string;
                return json ?? "[]";
            }
        }

        public static void JsonValue(object obj)
        {
            HttpContext.Current.Session[QConst.SESSION_MODULE_JSON_NAME] = obj;
        }

        public static void ListValue(object obj)
        {
            HttpContext.Current.Session[QConst.SESSION_MODULE_LIST_NAME] = obj;
        }

    }
}
