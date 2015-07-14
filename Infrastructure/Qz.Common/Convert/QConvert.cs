using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.Common
{
    public class QConvert
    {
        public static T ConvertTo<T>(object value)
        {
            T result;
            if (null == value || Convert.IsDBNull(value))
            {
                result = default(T);
            }
            else
            {
                if (!typeof(T).IsGenericType)
                {
                    result = (T)((object)Convert.ChangeType(value, typeof(T)));
                }
                else
                {
                    Type genericTypeDefinition = typeof(T).GetGenericTypeDefinition();
                    if (genericTypeDefinition != typeof(Nullable<>))
                    {
                        throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", value.GetType().FullName, typeof(T).FullName));
                    }
                    result = (T)((object)Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(T))));
                }
            }
            return result;

        }
    }
}
