using System;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Interface
{
    /// <summary>
    /// 字符串缓存处理类
    /// </summary>
    public interface IStringCache : ICache
    {
        /// <summary>
        /// 原子性递增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">增量：可以为负数</param>
        /// <returns>增长后的值</returns>
        double Increment(string key, double value = 1);

        /// <summary>
        /// 原子性递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        double Decrement(string key, double value = 1);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        RedisValue Get(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        T Get<T>(string key) where T : IModel;

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long Append(string key, RedisValue value);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool Set(string key, IModel value, DateTime expiry);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        bool Set(string key, IModel value, TimeSpan expiry);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool Set(string key, RedisValue value, DateTime expiry);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool Set(string key, RedisValue value, TimeSpan expiry);

        /// <summary>
        /// 原子性递增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">增量：可以为负数</param>
        /// <returns>增长后的值</returns>
        Task<double> IncrementAsync(string key, double value = 1);

        /// <summary>
        /// 原子性递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<double> DecrementAsync(string key, double value = 1);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<RedisValue> GetAsync(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key) where T : IModel;

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> AppendAsync(string key, RedisValue value);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, IModel value, DateTime expiry);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, IModel value, TimeSpan expiry);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, RedisValue value, DateTime expiry);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, RedisValue value, TimeSpan expiry);
    }
}