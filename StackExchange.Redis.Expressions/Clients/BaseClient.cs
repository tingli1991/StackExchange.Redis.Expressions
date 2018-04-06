namespace StackExchange.Redis.Expressions.Clients
{
    /// <summary>
    /// 客户端基础类
    /// </summary>
    public class BaseClient
    {
        /// <summary>
        /// 连接配置
        /// </summary>
        protected readonly ConnectionStringsSection _conf = null;

        /// <summary>
        /// Redis管理类
        /// </summary>
        protected static readonly ConnectionMultiplexer _instance = StackExchangeRedisManager.Instance;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseClient()
        {
            _conf = StackExchangeRedisManager.Config;
        }

        /// <summary>
        /// 这里的 MergeKey 用来拼接 Key 的前缀，具体不同的业务模块使用不同的前缀。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string MergeKey(string key)
        {
            return string.IsNullOrEmpty(_conf.ClientName) ? key : $"{_conf.ClientName}:{key}";
        }
    }
}