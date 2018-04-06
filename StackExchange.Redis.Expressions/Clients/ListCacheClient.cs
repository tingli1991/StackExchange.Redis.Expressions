using StackExchange.Redis.Expressions.Interface;
using StackExchange.Redis.Expressions.Serialize;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Clients
{
    /// <summary>
    /// List 缓存客户端
    /// </summary>
    public class ListCacheClient : CacheClient, IListCache
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public ListCacheClient() : base()
        {

        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="dbIndex"></param>
        public ListCacheClient(int dbIndex = 0) : base(dbIndex)
        {

        }

        /// <summary>
        /// 删除指定的List项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Remove(string key, IModel value, long count = 0)
        {
            return Execute(key, (newKey, db) => db.ListRemove(newKey, value.Serialize(), count));
        }

        /// <summary>
        /// 删除指定的List项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public long Remove(string key, RedisValue value, long count = 0)
        {
            return Execute(key, (newKey, db) => db.ListRemove(newKey, value, count));
        }

        /// <summary>
        /// 获取指定索引的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public RedisValue Get(string key, long index)
        {
            return Execute(key, (newKey, db) => db.ListGetByIndex(newKey, index));
        }

        /// <summary>
        /// 获取指定索引的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public T Get<T>(string key, long index) where T : IModel
        {
            return Execute(key, (newKey, db) => db.ListGetByIndex(newKey, index).DeSerialize<T>());
        }

        /// <summary>
        /// 获取指定范围的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public List<RedisValue> GetRange(string key, long start = 0, long stop = -1)
        {
            return Execute(key, (newKey, db) => db.ListRange(newKey, start, stop).ToList());
        }

        /// <summary>
        /// 获取指定范围的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public List<T> GetRange<T>(string key, long start = 0, long stop = -1) where T : IModel
        {
            return Execute(key, (newKey, db) => db.ListRange(newKey, start, stop).Select(e => e.DeSerialize<T>()).ToList());
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        public long RightPush(string key, IModel value, When when = When.Always)
        {
            return Execute(key, (newKey, db) => db.ListRightPush(newKey, value.Serialize(), when));
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        public long RightPush(string key, RedisValue value, When when = When.Always)
        {
            return Execute(key, (newKey, db) => db.ListRightPush(newKey, value, when));
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T RightPop<T>(string key) where T : IModel
        {
            return Execute(key, (newKey, db) => db.ListRightPop(newKey).DeSerialize<T>());
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedisValue RightPop(string key)
        {
            return Execute(key, (newKey, db) => db.ListRightPop(newKey));
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public long LeftPush(string key, IModel value, When when = When.Always)
        {
            return Execute(key, (newKey, db) => db.ListLeftPush(newKey, value.Serialize(), when));
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public long LeftPush(string key, RedisValue value, When when = When.Always)
        {
            return Execute(key, (newKey, db) => db.ListLeftPush(newKey, value, when));
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T LeftPop<T>(string key) where T : IModel
        {
            return Execute(key, (newKey, db) => db.ListLeftPop(newKey).DeSerialize<T>());
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedisValue LeftPop(string key)
        {
            return Execute(key, (newKey, db) => db.ListLeftPop(newKey));
        }

        /// <summary>
        /// 长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long Length(string key)
        {
            return Execute(key, (newKey, db) => db.ListLength(newKey));
        }

        /// <summary>
        /// 删除指定的List项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<long> RemoveAsync(string key, IModel value, long count = 0)
        {
            return await Execute(key, (newKey, db) => db.ListRemoveAsync(newKey, value.Serialize(), count));
        }

        /// <summary>
        /// 删除指定的List项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<long> RemoveAsync(string key, RedisValue value, long count = 0)
        {
            return await Execute(key, (newKey, db) => db.ListRemoveAsync(newKey, value, count));
        }

        /// <summary>
        /// 获取指定索引的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<RedisValue> GetAsync(string key, long index)
        {
            return await Execute(key, (newKey, db) => db.ListGetByIndexAsync(newKey, index));
        }

        /// <summary>
        /// 获取指定索引的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, long index) where T : IModel
        {
            var value = await Execute(key, (newKey, db) => db.ListGetByIndexAsync(newKey, index));
            return value.DeSerialize<T>();
        }

        /// <summary>
        /// 获取指定范围的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<List<RedisValue>> GetRangeAsync(string key, long start = 0, long stop = -1)
        {
            var values = await Execute(key, (newKey, db) => db.ListRangeAsync(newKey, start, stop));
            return values.ToList();
        }

        /// <summary>
        /// 获取指定范围的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<List<T>> GetRangeAsync<T>(string key, long start = 0, long stop = -1) where T : IModel
        {
            var values = await Execute(key, (newKey, db) => db.ListRangeAsync(newKey, start, stop));
            return values.Select(e => e.DeSerialize<T>()).ToList();
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        public async Task<long> RightPushAsync(string key, IModel value, When when = When.Always)
        {
            return await Execute(key, (newKey, db) => db.ListRightPushAsync(newKey, value.Serialize(), when));
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        public async Task<long> RightPushAsync(string key, RedisValue value, When when = When.Always)
        {
            return await Execute(key, (newKey, db) => db.ListRightPushAsync(newKey, value, when));
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> RightPopAsync<T>(string key) where T : IModel
        {
            var value = await Execute(key, (newKey, db) => db.ListRightPopAsync(newKey));
            return value.DeSerialize<T>();
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<RedisValue> RightPopAsync(string key)
        {
            return await Execute(key, (newKey, db) => db.ListRightPopAsync(newKey));
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public async Task<long> LeftPushAsync(string key, IModel value, When when = When.Always)
        {
            return await Execute(key, (newKey, db) => db.ListLeftPushAsync(newKey, value.Serialize(), when));
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public async Task<long> LeftPushAsync(string key, RedisValue value, When when = When.Always)
        {
            return await Execute(key, (newKey, db) => db.ListLeftPushAsync(newKey, value, when));
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> LeftPopAsync<T>(string key) where T : IModel
        {
            var value = await Execute(key, (newKey, db) => db.ListLeftPopAsync(newKey));
            return value.DeSerialize<T>();
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<RedisValue> LeftPopAsync(string key)
        {
            return await Execute(key, (newKey, db) => db.ListLeftPopAsync(newKey));
        }

        /// <summary>
        /// 长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> LengthAsync(string key)
        {
            return await Execute(key, (newKey, db) => db.ListLengthAsync(newKey));
        }
    }
}