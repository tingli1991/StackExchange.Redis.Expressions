using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Interface
{
    /// <summary>
    /// 连表类型缓存
    /// </summary>
    public interface IListCache : ICache
    {
        /// <summary>
        /// 删除指定的List项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        long Remove(string key, IModel value, long count = 0);

        /// <summary>
        /// 删除指定的List项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        long Remove(string key, RedisValue value, long count = 0);

        /// <summary>
        /// 获取指定索引的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        RedisValue Get(string key, long index);

        /// <summary>
        /// 获取指定索引的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        T Get<T>(string key, long index) where T : IModel;

        /// <summary>
        /// 获取指定范围的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        List<RedisValue> GetRange(string key, long start = 0, long stop = -1);

        /// <summary>
        /// 获取指定范围的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        List<T> GetRange<T>(string key, long start = 0, long stop = -1) where T : IModel;

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        long RightPush(string key, IModel value, When when = When.Always);

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        long RightPush(string key, RedisValue value, When when = When.Always);

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T RightPop<T>(string key) where T : IModel;

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        RedisValue RightPop(string key);

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long LeftPush(string key, IModel value, When when = When.Always);

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        long LeftPush(string key, RedisValue value, When when = When.Always);

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T LeftPop<T>(string key) where T : IModel;

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        RedisValue LeftPop(string key);

        /// <summary>
        /// 长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long Length(string key);

        /// <summary>
        /// 删除指定的List项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<long> RemoveAsync(string key, IModel value, long count = 0);

        /// <summary>
        /// 删除指定的List项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<long> RemoveAsync(string key, RedisValue value, long count = 0);

        /// <summary>
        /// 获取指定索引的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<RedisValue> GetAsync(string key, long index);

        /// <summary>
        /// 获取指定索引的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, long index) where T : IModel;

        /// <summary>
        /// 获取指定范围的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<List<RedisValue>> GetRangeAsync(string key, long start = 0, long stop = -1);

        /// <summary>
        /// 获取指定范围的list值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<List<T>> GetRangeAsync<T>(string key, long start = 0, long stop = -1) where T : IModel;

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        Task<long> RightPushAsync(string key, IModel value, When when = When.Always);

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        Task<long> RightPushAsync(string key, RedisValue value, When when = When.Always);

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> RightPopAsync<T>(string key) where T : IModel;

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValue> RightPopAsync(string key);

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        Task<long> LeftPushAsync(string key, IModel value, When when = When.Always);

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        Task<long> LeftPushAsync(string key, RedisValue value, When when = When.Always);

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> LeftPopAsync<T>(string key) where T : IModel;

        /// <summary>
        /// 出栈
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValue> LeftPopAsync(string key);

        /// <summary>
        /// 长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> LengthAsync(string key);
    }
}