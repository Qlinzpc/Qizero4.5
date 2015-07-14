

namespace Qz.GPS.DirectService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Qz.Core.Infrastructure.Interface.UnitOfWorks;
    using Qz.UnitOfWorks;
    using Qz.GPS.Models;
    using Qz.Common;

    public class GPSService
    {
        private static IUnitOfWorks works;

        /// <summary>
        /// 获得工作单元 实例 
        /// </summary>
        /// <param name="isOwn">是否是本身上下文对象</param>
        /// <returns></returns>
        public static IUnitOfWorks Instance(bool isOwn = false)
        {
            if (works == null)
            {
                works = new UnitOfWorks<GPSContext>(new GPSContext());
                works.Config(null);
            }

            works.IsOwnContext = isOwn;

            return works;
        }

    }
}
