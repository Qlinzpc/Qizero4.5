
namespace Qz.Core.Infrastructure.Configuration
{
    using System;

    using Qz.Core.Infrastructure.Interface.Configuration;

    public class DBConfiguration : IConfiguration
    {
        /// <summary>
        /// 启用自动检测变化
        /// </summary>
        public bool AutoDetectChangesEnabled { get; set; }
        /// <summary>
        /// 延迟加载功能
        /// </summary>
        public bool LazyLoadingEnabled { get; set; }
        /// <summary>
        /// 创建启用代理
        /// </summary>
        public bool ProxyCreationEnabled { get; set; }
        /// <summary>
        /// 验证保存功能
        /// </summary>
        public bool ValidateOnSaveEnabled { get; set; }
    }
}
