
namespace Qz.Common
{
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    public class Config
    {
        /// <summary>
        /// 是否处于调试状态 
        /// </summary>
        public static bool IsDebug = Debug();
        
        /// <summary>
        /// 百度翻译API ( 授权 API key )
        /// </summary>
        public static string TranslateKey = ConfigurationManager.AppSettings["TranslateKey"].ToString();
        /// <summary>
        /// 百度翻译API Url 
        /// </summary>
        public static string TranslateUrl = ConfigurationManager.AppSettings["TranslateUrl"].ToString();

        private static bool Debug()
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("");

            SystemWebSectionGroup ws = (SystemWebSectionGroup)configuration.GetSectionGroup("system.web");
            CompilationSection cp = ws.Compilation;

            return cp.Debug;
        }

    }
}
