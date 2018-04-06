using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Interface
{
    /// <summary>
    /// Hash缓存接口
    /// </summary>
    public interface IHashCache : ICache
    {
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        bool Exists(string key, string hashField);

        /// <summary>
        /// 删除Hash中的某个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        bool Remove(string key, string hashField);

        /// <summary>
        /// 原子性递增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value">增量：可以为负数</param>
        /// <returns>增长后的值</returns>
        double Increment(string key, string hashField, double value = 1);

        /// <summary>
        /// 原子性递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        double Decrement(string key, string hashField, double value = 1);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        RedisValue Get(string key, string hashField);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        T Get<T>(string key, string hashField) where T : IModel;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set(string key, string hashField, RedisValue value);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        bool Set(string key, string hashField, RedisValue value, When when = When.Always);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        bool Set(string key, string hashField, IModel value, When when = When.Always);

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key, string hashField);

        /// <summary>
        /// 删除Hash中的某个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key, string hashField);

        /// <summary>
        /// 原子性递增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value">增量：可以为负数</param>
        /// <returns>增长后的值</returns>
        Task<double> IncrementAsync(string key, string hashField, double value = 1);

        /// <summary>
        /// 原子性递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<double> DecrementAsync(string key, string hashField, double value = 1);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<RedisValue> GetAsync(string key, string hashField);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, string hashField) where T : IModel;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, string hashField, RedisValue value, When when = When.Always);
        
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, string hashField, IModel value, When when = When.Always);
    }
}