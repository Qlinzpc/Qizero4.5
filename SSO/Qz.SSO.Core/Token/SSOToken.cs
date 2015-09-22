using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qz.SSO.Core.Entity;

namespace Qz.SSO.Core.Token
{
    public class SsoToken
    {
        public string Id { get; set; }

        public SsoUser User { get; set; }

    }
}
