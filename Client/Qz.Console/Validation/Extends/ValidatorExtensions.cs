using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.Console.Validation
{
    public class NotEmptyNull : PropertyValidator
    {
        public NotEmptyNull()
            : base("{PropertyName} 不能为空 !")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var str = context.PropertyValue as string;

            if (str != null)
            {
                str = str.Trim();
            }

            return !string.IsNullOrEmpty(str);
        }
    }
}
