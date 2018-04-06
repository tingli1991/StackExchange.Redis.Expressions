using System;
using System.Configuration;

namespace StackExchange.Redis.Expressions
{
    /// <summary>
    /// 连接字符串配置
    /// </summary>
    public class ConnectionStringsSection : ConfigurationSection
    {
        /// <summary>
        /// 主机列表地址
        /// </summary>
        [ConfigurationProperty("hosts", DefaultValue = "127.0.0.1:6379")]
        public string Hosts
        {
            get => $"{base["hosts"]}";
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        [ConfigurationProperty("password", DefaultValue = "")]
        public string Password
        {
            get => $"{base["password"]}";
        }

        /// <summary>
        /// 连接超时时间（单位：毫秒秒）
        /// </summary>
        [ConfigurationProperty("connectTimeout", DefaultValue = 3000)]
        public int ConnectTimeOut
        {
            get => Convert.ToUInt16(base["connectTimeout"]);
        }

        /// <summary>
        /// 在redis内用于判别不同链接客户端
        /// </summary>
        [ConfigurationProperty("name", DefaultValue = "")]
        public string ClientName
        {
            get => $"{base["name"]}";
        }

        /// <summary>
        /// 如果是true，Connect方法在链接不到有效的服务器的时候不会创建一个链接实例
        /// </summary>
        [ConfigurationProperty("abortOnConnectFail", DefaultValue = false)]
        public bool AbortOnConnectFail
        {
            get => Convert.ToBoolean(base["abortOnConnectFail"]);
        }

        /// <summary>
        /// 启用被认定为是有风险的一些命令
        /// </summary>
        [ConfigurationProperty("allowAdmin", DefaultValue = false)]
        public bool AllowAdmin
        {
            get => Convert.ToBoolean(base["allowAdmin"]);
        }

        /// <summary>
        /// 默认数据库索引, 从 0 到 databases - 1
        /// </summary>
        [ConfigurationProperty("defaultDatabase", DefaultValue = 0)]
        public int DefaultDatabase
        {
            get => Convert.ToInt32(base["defaultDatabase"]);
        }

        /// <summary>
        /// 异步超时设置(单位：秒)
        /// </summary>
        [ConfigurationProperty("syncTimeout", DefaultValue = 3)]
        public int SyncTimeout
        {
            get => Convert.ToInt32(base["syncTimeout"]);
        }

        /// <summary>
        /// 代理方式
        /// </summary>
        [ConfigurationProperty("proxy", DefaultValue = Proxy.None)]
        public Proxy Proxy
        {
            get
            {
                var value = $"{base["proxy"]}";
                if (Enum.TryParse<Proxy>(value, out var proxy))
                {
                    return proxy;
                }
                else
                {
                    return Proxy.None;
                }
            }
        }
    }
}