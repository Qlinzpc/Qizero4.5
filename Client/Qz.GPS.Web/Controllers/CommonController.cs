using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Qz.GPS.Web.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 汉译英 英译汉翻译
        /// </summary>
        /// <param name="q">待翻译字符</param>
        /// <param name="r">翻译结果类型 ( zh中文 en英文 ) </param>
        /// <returns>翻译后的结果</returns>
        public string Translate(string q, string r = "zh")
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var from = "zh";
            var to = "en";

            switch (r)
            {
                case "zh":
                    from = "en"; to = "zh";
                    break;
                case "en":
                    from = "zh"; to = "en";
                    break;
            }

            var key = Qz.Common.Config.TranslateKey;

            var requestUri = string.Format(Qz.Common.Config.TranslateUrl + "?client_id={0}&q={1}&from={2}&to={3}",
                key,
                q,
                from,
                to);

            HttpClient client = new HttpClient();

            var result = client.GetStringAsync(requestUri).Result;

            return result;
        }

    }
}
