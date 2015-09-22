using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using Qz.SSO.Core;
using Qz.SSO.Core.Entity;
using Qz.SSO.Core.Token;

namespace Qz.SSO.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“AuthService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 AuthService.svc 或 AuthService.svc.cs，然后开始调试。
    public class AuthService : IAuthService
    {
        public void DoWork()
        {
        }


        public Core.Token.SsoToken GetSsoToken(string token)
        {
            return Security.SsoTokens.Find(x => x.Id.Equals(token));
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }

        public string Login(string name, string pwd)
        {
            try
            {
                var user = Security.Login(name, pwd);

                var token = new SsoToken
                {
                    Id = Guid.NewGuid().ToString("N"),
                    User = user
                };

                Security.SsoTokens.Add(token);

                return token.Id;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
    }
}
