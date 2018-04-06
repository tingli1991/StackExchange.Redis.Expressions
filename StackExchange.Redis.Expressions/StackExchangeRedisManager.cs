using log4net;
using System;
using System.Configuration;

namespace StackExchange.Redis.Expressions
{
    /// <summary>
    /// StackExchangeRedisManager 连接管理器
    /// 
    /// 在StackExchange.Redis中最重要的对象是ConnectionMultiplexer类， 它存在于StackExchange.Redis命名空间中。
    /// 这个类隐藏了Redis服务的操作细节，ConnectionMultiplexer类做了很多东西， 在所有调用之间它被设计为共享和重用的。
    /// 不应该为每一个操作都创建一个ConnectionMultiplexer 。 ConnectionMultiplexer是线程安全的 ， 推荐使用下面的方法。
    /// 在所有后续示例中 ， 都假定你已经实例化好了一个ConnectionMultiplexer类，它将会一直被重用 ，
    /// 现在我们来创建一个ConnectionMultiplexer实例。它是通过ConnectionMultiplexer.Connect 或者 ConnectionMultiplexer.ConnectAsync，
    /// 传递一个连接字符串或者一个ConfigurationOptions 对象来创建的。
    /// 连接字符串可以是以逗号分割的多个服务的节点.
    /// 
    /// 
    /// 注意 : 
    /// ConnectionMultiplexer 实现了IDisposable接口当我们不再需要是可以将其释放的 , 这里我故意不使用 using 来释放他。 
    /// 简单来讲创建一个ConnectionMultiplexer是十分昂贵的 ， 一个好的主意是我们一直重用一个ConnectionMultiplexer对象。
    /// 一个复杂的的场景中可能包含有主从复制 ， 对于这种情况，只需要指定所有地址在连接字符串中（它将会自动识别出主服务器）
    ///  ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("server1:6379,server2:6379");
    /// 假设这里找到了两台主服务器，将会对两台服务进行裁决选出一台作为主服务器来解决这个问题 ， 这种情况是非常罕见的 ，我们也应该避免这种情况的发生。
    /// 
    /// 
    /// 这里有个和 ServiceStack.Redis 大的区别是没有默认的连接池管理了。没有连接池自然有其利弊,最大的好处在于等待获取连接的等待时间没有了,
    /// 也不会因为连接池里面的连接由于没有正确释放等原因导致无限等待而处于死锁状态。缺点在于一些低质量的代码可能导致服务器资源耗尽。不过提供连接池等阻塞和等待的手段是和作者的设计理念相违背的。StackExchange.Redis这里使用管道和多路复用的技术来实现减少连接
    /// 
    /// 参考：http://www.cnblogs.com/Leo_wl/p/4968537.html
    /// </summary>
    public class StackExchangeRedisManager
    {
        private static ConnectionMultiplexer _instance;
        private static readonly object _lock = new object();
        private const string SECTION_NAME = "redisConnectionStrings";
        private static readonly ILog _log = LogManager.GetLogger(typeof(StackExchangeRedisManager));
        public static readonly ConnectionStringsSection Config = (ConnectionStringsSection)ConfigurationManager.GetSection(SECTION_NAME);

        /// <summary>
        /// 单例获取
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null || !_instance.IsConnected)
                {
                    lock (_lock)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = GetManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return Instance.GetDatabase();
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        private static ConnectionMultiplexer GetManager()
        {
            var configOptions = new ConfigurationOptions()
            {
                Proxy = Config.Proxy,
                Password = Config.Password,
                AllowAdmin = Config.AllowAdmin,
                ClientName = Config.ClientName,
                SyncTimeout = Config.SyncTimeout,
                ResponseTimeout = Config.SyncTimeout,
                ConnectTimeout = Config.ConnectTimeOut,
                DefaultDatabase = Config.DefaultDatabase,
                AbortOnConnectFail = Config.AbortOnConnectFail
            };

            var hosts = Config.Hosts.Split(',');
            foreach (var host in hosts)
            {
                var hostArray = host.Split(':');
                var hostName = hostArray[0];
                var port = Convert.ToInt32(hostArray[1]);
                configOptions.EndPoints.Add(hostName, port);
            }
            var connect = ConnectionMultiplexer.Connect(configOptions);

            //注册如下事件
            connect.ConnectionFailed += MuxerConnectionFailed;
            connect.ConnectionRestored += MuxerConnectionRestored;
            connect.ErrorMessage += MuxerErrorMessage;
            connect.ConfigurationChanged += MuxerConfigurationChanged;
            connect.HashSlotMoved += MuxerHashSlotMoved;
            connect.InternalError += MuxerInternalError;
            return connect;
        }
        #region 事件
        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            _log.Error($"Configuration changed: {e.EndPoint}");
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            _log.Error($"ErrorMessage: {e.Message}");
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            _log.Error($"ConnectionRestored:{e.EndPoint}");
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _log.Error($"重新连接：Endpoint failed: {e.EndPoint},{ e.FailureType},{(e.Exception == null ? "" : (", " + e.Exception.Message))}");
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            _log.Error($"HashSlotMoved:NewEndPoint{e.NewEndPoint}, OldEndPoint:{e.OldEndPoint}");
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            _log.Error("InternalError:Message:", e.Exception);
        }
        #endregion 事件
    }
}