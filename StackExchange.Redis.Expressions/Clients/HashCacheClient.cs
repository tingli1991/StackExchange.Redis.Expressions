using StackExchange.Redis.Expressions.Interface;
using StackExchange.Redis.Expressions.Serialize;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Clients
{
    /// <summary>
    /// Hash缓存客户端
    /// </summary>
    public class HashCacheClient : CacheClient, IHashCache
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public HashCacheClient() : base()
        {

        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="dbIndex"></param>
        public HashCacheClient(int dbIndex = 0) : base(dbIndex)
        {

        }

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool Exists(string key, string hashField)
        {
            return Execute(key, (newKey, db) => db.HashExists(newKey, hashField));
        }

        /// <summary>
        /// 删除Hash中的某个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool Remove(string key, string hashField)
        {
            return Execute(key, (newKey, db) => db.HashDelete(newKey, hashField));
        }

        /// <summary>
        /// 原子性递增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value">增量：可以为负数</param>
        /// <returns>增长后的值</returns>
        public double Increment(string key, string hashField, double value = 1)
        {
            return Execute(key, (newKey, db) => db.HashIncrement(newKey, hashField, value));
        }

        /// <summary>
        /// 原子性递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Decrement(string key, string hashField, double value = 1)
        {
            return Execute(key, (newKey, db) => db.HashDecrement(newKey, hashField, value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public RedisValue Get(string key, string hashField)
        {
            return Execute(key, (newKey, db) => db.HashGet(newKey, hashField));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Get<T>(string key, string hashField) where T : IModel
        {
            return Execute(key, (newKey, db) => db.HashGet(newKey, hashField).DeSerialize<T>());
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, string hashField, RedisValue value)
        {
            return Execute(key, (newKey, db) => db.HashSet(newKey, hashField, value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public bool Set(string key, string hashField, RedisValue value, When when = When.Always)
        {
            return Execute(key, (newKey, db) => db.HashSet(newKey, hashField, value, when));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public bool Set(string key, string hashField, IModel value, When when = When.Always)
        {
            return Execute(key, (newKey, db) => db.HashSet(newKey, hashField, value.Serialize()));
        }

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key, string hashField)
        {
            return await Execute(key, (newKey, db) => db.HashExistsAsync(newKey, hashField));
        }

        /// <summary>
        /// 删除Hash中的某个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key, string hashField)
        {
            return await Execute(key, (newKey, db) => db.HashDeleteAsync(newKey, hashField));
        }

        /// <summary>
        /// 原子性递增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value">增量：可以为负数</param>
        /// <returns>增长后的值</returns>
        public async Task<double> IncrementAsync(string key, string hashField, double value = 1)
        {
            return await Execute(key, (newKey, db) => db.HashIncrementAsync(newKey, hashField, value));
        }

        /// <summary>
        /// 原子性递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<double> DecrementAsync(string key, string hashField, double value = 1)
        {
            return await Execute(key, (newKey, db) => db.HashDecrementAsync(newKey, hashField, value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<RedisValue> GetAsync(string key, string hashField)
        {
            return await Execute(key, (newKey, db) => db.HashGetAsync(newKey, hashField));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, string hashField) where T : IModel
        {
            var value = await Execute(key, (newKey, db) => db.HashGetAsync(newKey, hashField));
            return value.DeSerialize<T>();
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, string hashField, RedisValue value, When when = When.Always)
        {
            return await Execute(key, (newKey, db) => db.HashSetAsync(newKey, hashField, value, when));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, string hashField, IModel value, When when = When.Always)
        {
            return await Execute(key, (newKey, db) => db.HashSetAsync(newKey, hashField, value.Serialize(), when));
        }
    }
}