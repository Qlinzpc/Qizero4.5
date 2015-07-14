
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

    public class SessionUser
    {
        public static User Data
        {
            get
            {
                var user = HttpContext.Current.Session[QConst.SESSION_USER_NAME] as User;

                user = user ?? new User()
                {
                    Id = 0
                };

                if(user.Id == 0) HttpContext.Current.Response.Redirect(QConst.LOGIN_URL, true);

                return user;
            }
        }

        public static void Value(object obj)
        {
            HttpContext.Current.Session[QConst.SESSION_USER_NAME] = obj;
        }

    }
}
