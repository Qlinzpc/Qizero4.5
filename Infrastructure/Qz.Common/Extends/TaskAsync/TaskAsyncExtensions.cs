using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.Common
{
    public static class TaskAsyncExtensions
    {
        public static async void RunAsync(this Action function, Action callback = null)
        {
            Func<Task> func = () =>
            {
                return Task.Run(function);
            };

            await func();

            if (callback != null) callback();
        }

        public static async void RunAsync<TResult>(this Func<TResult> function, Action<TResult> callback = null)
        {

            Func<Task<TResult>> func = () =>
            {
                return Task.Run(function);
            };

            TResult result = await func();

            if (callback != null) callback(result);

        }

    }
}
