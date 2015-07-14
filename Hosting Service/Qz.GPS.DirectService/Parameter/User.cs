using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.GPS.DirectService.Parameter
{
    public class User
    {
        public class ByAccountAndPassword
        {
            public string Account { get; set; }
            public string Password { get; set; }
        }

        public class Login
        {
            public string Account { get; set; }
            public string Password { get; set; }
        }

    }
}
