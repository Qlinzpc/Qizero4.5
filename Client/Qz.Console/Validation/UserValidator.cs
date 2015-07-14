
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Qz.Console.Models;

namespace Qz.Console.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {

            // 名称为 LoginRule 的规则组 
            RuleSet("LoginRule", () =>
            {
                // 账号 Account 不能为 空 
                RuleFor(x => x.Account).SetValidator(new NotEmptyNull());

                // 密码 Password 不能为 空 
                RuleFor(x => x.Password).SetValidator(new NotEmptyNull()).WithMessage("密码 Password 不能为 空 !");
            });

            RuleSet("AddRule", () =>
            {
                RuleFor(x => x.UserName).SetValidator(new NotEmptyNull()).WithMessage("用户名 不能为 空 !");

            });
        }

    }
}
