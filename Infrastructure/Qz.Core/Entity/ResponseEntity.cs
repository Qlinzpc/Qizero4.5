using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.Core.Entity
{

    public class Response
    {
        public string Times { get; set; }

        private int status;
        /// <summary>
        /// 0成功 1失败 
        /// </summary>
        public int Status
        {
            get
            {
                return status < 0 ? 0 : status;
            }
            set
            {
                status = value;
            }
        }

        public string Message { get; set; }
    }

    public class Response<T> : Response where T : class
    {

        public T Data { get; set; }
    }

}
