
namespace Qz.GPS.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    using Qz.Common;

    public partial class SysLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string Action { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// ÈÕÆÚ×ª»» ( yyyy-MM-dd hh:mm:ss )
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public System.DateTime CreateDate { get; set; }
    }
}
