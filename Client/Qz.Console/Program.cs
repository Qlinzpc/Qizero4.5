
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Qz.Console.Models;
using Qz.Console.Validation;
using Qz.Common;
using FluentValidation.Internal;
using FluentValidation;

namespace Qz.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            var user = new User()
            {
                UserName = "",
                Account = ""
            };

            var validator = new UserValidator();

            var context = new ValidationContext<User>(
                user,
                new PropertyChain(),
                new RulesetValidatorSelector(new string[]{
                    "LoginRule",
                    "AddRule"
                }));

            var results = validator.Validate(context);

            if (!results.IsValid)
            {
                System.Console.WriteLine(QJsonConvert.Serialize(user));
                // 遍历所有失败的信息 
                foreach (var failure in results.Errors)
                {
                    System.Console.WriteLine("验证失败的 Property {0}, 错误信息: {1}".QFormat(
                        failure.PropertyName,
                        failure.ErrorMessage));
                }
            }

            System.Console.ReadKey();
        }
    }
}
