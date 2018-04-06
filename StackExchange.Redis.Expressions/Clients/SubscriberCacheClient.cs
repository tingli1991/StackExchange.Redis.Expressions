using log4net;
using StackExchange.Redis.Expressions.Interface;
using StackExchange.Redis.Expressions.Serialize;
using System;

namespace StackExchange.Redis.Expressions.Clients
{
    /// <summary>
    /// 
    /// </summary>
    public class SubscriberCacheClient : BaseClient
    {
        private static ISubscriber _sub = null;
        private static readonly ILog _log = LogManager.GetLogger(typeof(SubscriberCacheClient));

        /// <summary>
        /// 初始化参数
        /// </summary>
        public SubscriberCacheClient()
        {
            _sub = _instance.GetSubscriber();
        }

        /// <summary>
        /// Redis发布订阅  发布
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="messageBody"></param>
        /// <returns></returns>
        public long Publish<T>(string channel, T messageBody) where T : IModel
        {
            return _sub.Publish(channel, messageBody.Serialize());
        }

        /// <summary>
        /// Redis发布订阅  订阅
        /// </summary>
        /// <param name="subChannel"></param>
        /// <param name="handler"></param>
        public void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null)
        {
            _sub.Subscribe(subChannel, (channel, messageBody) =>
            {
                if (handler == null)
                {
                    _log.Error(subChannel + " 订阅收到消息：" + messageBody);
                }
                else
                {
                    //订阅成功
                    handler(channel, messageBody);
                }
            });
        }

        /// <summary>
        /// Redis发布订阅  取消订阅
        /// </summary>
        /// <param name="channel"></param>
        public void Unsubscribe(string channel)
        {
            ISubscriber sub = _instance.GetSubscriber();
            sub.Unsubscribe(channel);
        }

        /// <summary>
        /// Redis发布订阅  取消全部订阅
        /// </summary>
        public void UnsubscribeAll()
        {
            ISubscriber sub = _instance.GetSubscriber();
            sub.UnsubscribeAll();
        }
    }
}