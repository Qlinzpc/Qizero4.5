
namespace Qz.Common
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// JsonConvert 序列化 , 反序列化 
    /// </summary>
    public class QJsonConvert
    {
        /// <summary>
        /// 将对象 序列化为字符串 
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            if( obj == null || "".Equals( obj ) ) return "";

            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,  // 忽略 null , 序列化 
                Formatting = Formatting.None
                // Formatting = Formatting.None    // 不进行格式化 JSON  
            });
        }

        /// <summary>
        /// 将字符串 反序列化为指定对象  
        /// </summary>
        /// <typeparam name="T">指定对象</typeparam>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string value) where T : class
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

    }

}
