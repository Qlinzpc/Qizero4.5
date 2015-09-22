using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Qz.SSO.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IAuthService”。
    [ServiceContract]
    public interface IAuthService
    {

        [OperationContract]
        Qz.SSO.Core.Token.SsoToken GetSsoToken(string token);

        [OperationContract]
        bool ValidateToken(string token);

        [OperationContract]
        string Login(string name, string pwd);
    }
}
