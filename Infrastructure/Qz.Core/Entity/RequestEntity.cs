
namespace Qz.Core.Entity
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using Qz.Common;

    public class Request
    {
        /// <summary>
        /// 请求开始时间 
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime BeginTime { get; set; }

        public string Message { get; set; }

        public Request()
        {
            this.BeginTime = DateTime.Now;
        }
    }

    public class Request<T> : Request where T : class
    {
        public T Obj { get; set; }

        public Request() : base()
        {
        }
    }

}
