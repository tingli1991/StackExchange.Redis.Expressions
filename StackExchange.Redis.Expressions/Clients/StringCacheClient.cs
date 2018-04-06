using StackExchange.Redis.Expressions.Interface;
using StackExchange.Redis.Expressions.Serialize;
using System;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Clients
{
    /// <summary>
    /// Strin缓存客户端
    /// </summary>
    public class StringCacheClient : CacheClient, IStringCache
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public StringCacheClient() : base()
        {

        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="dbIndex"></param>
        public StringCacheClient(int dbIndex = 0) : base(dbIndex)
        {

        }

        /// <summary>
        /// 原子性递增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">增量：可以为负数</param>
        /// <returns>增长后的值</returns>
        public double Increment(string key, double value = 1)
        {
            return Execute(key, (newKey, db) => db.StringIncrement(newKey, value));
        }

        /// <summary>
        /// 原子性递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Decrement(string key, double value = 1)
        {
            return Execute(key, (newKey, db) => db.StringDecrement(newKey, value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public RedisValue Get(string key)
        {
            return Execute(key, (newKey, db) => db.StringGet(newKey));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public T Get<T>(string key) where T : IModel
        {
            return Execute(key, (newKey, db) => db.StringGet(newKey).DeSerialize<T>());
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Append(string key, RedisValue value)
        {
            return Execute(key, (newKey, db) => db.StringAppend(newKey, value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set(string key, IModel value, DateTime expiry)
        {
            return Execute(key, (newKey, db) =>
            {
                var isSuccess = db.StringSet(newKey, value.Serialize());
                isSuccess = SetExpiryTime(newKey, expiry);
                return isSuccess;
            });
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set(string key, IModel value, TimeSpan expiry)
        {
            return Execute(key, (newKey, db) =>
            {
                var isSuccess = db.StringSet(newKey, value.Serialize());
                isSuccess = db.KeyExpire(newKey, expiry);
                return isSuccess;
            });
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set(string key, RedisValue value, DateTime expiry)
        {
            return Execute(key, (newKey, db) =>
            {
                var isSuccess = db.StringSet(newKey, value);
                isSuccess = SetExpiryTime(newKey, expiry);
                return isSuccess;
            });
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set(string key, RedisValue value, TimeSpan expiry)
        {
            return Execute(key, (newKey, db) =>
            {
                var isSuccess = db.StringSet(newKey, value);
                isSuccess = SetExpiryTime(newKey, expiry);
                return isSuccess;
            });
        }

        /// <summary>
        /// 原子性递增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">增量：可以为负数</param>
        /// <returns>增长后的值</returns>
        public async Task<double> IncrementAsync(string key, double value = 1)
        {
            return await Execute(key, (newKey, db) => db.StringIncrementAsync(newKey, value));
        }

        /// <summary>
        /// 原子性递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<double> DecrementAsync(string key, double value = 1)
        {
            return await Execute(key, (newKey, db) => db.StringDecrementAsync(newKey, value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<RedisValue> GetAsync(string key)
        {
            return await Execute(key, (newKey, db) => db.StringGetAsync(newKey));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key) where T : IModel
        {
            var result = await Execute(key, (newKey, db) => db.StringGetAsync(newKey));
            return result.DeSerialize<T>();
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> AppendAsync(string key, RedisValue value)
        {
            return await Execute(key, (newKey, db) => db.StringAppendAsync(newKey, value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, IModel value, DateTime expiry)
        {
            return await Execute(key, (newKey, db) =>
            {
                var isSuccess = db.StringSetAsync(newKey, value.Serialize());
                isSuccess = db.KeyExpireAsync(newKey, expiry);
                return isSuccess;
            });
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, IModel value, TimeSpan expiry)
        {
            return await Execute(key, (newKey, db) =>
            {
                var isSuccess = db.StringSetAsync(newKey, value.Serialize());
                isSuccess = db.KeyExpireAsync(newKey, expiry);
                return isSuccess;
            });
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, RedisValue value, DateTime expiry)
        {
            return await Execute(key, (newKey, db) =>
            {
                var isSuccess = db.StringSetAsync(newKey, value);
                isSuccess = db.KeyExpireAsync(newKey, expiry);
                return isSuccess;
            });
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, RedisValue value, TimeSpan expiry)
        {
            return await Execute(key, (newKey, db) =>
            {
                var isSuccess = db.StringSetAsync(newKey, value);
                isSuccess = db.KeyExpireAsync(newKey, expiry);
                return isSuccess;
            });
        }
    }
}