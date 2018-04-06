using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Interface
{
    /// <summary>
    /// 结合类型缓存
    /// </summary>
    public interface ISetCache : ICache
    {
        /// <summary>
        /// 设置集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set(string key, RedisValue value);

        /// <summary>
        /// 设置集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value) where T : IModel;

        /// <summary>
        /// 批量设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        long Set(string key, params RedisValue[] values);

        /// <summary>
        /// 批量设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        long Set<T>(string key, params T[] values) where T : IModel;

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <param name="operation">
        /// 0：求并集
        /// 1：求交集
        /// 2：求差集
        /// </param>
        /// <param name="firstKey">第一个Key</param>
        /// <param name="secondKey">第二个Key</param>
        /// <returns></returns>
        List<RedisValue> Combine(SetOperation operation, string firstKey, string secondKey);

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <param name="operation">
        /// 0：求并集
        /// 1：求交集
        /// 2：求差集
        /// </param>
        /// <param name="firstKey">第一个Key</param>
        /// <param name="secondKey">第二个Key</param>
        /// <returns></returns>
        List<T> Combine<T>(SetOperation operation, string firstKey, string secondKey) where T : IModel;

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <param name="operation">
        /// 0：求并集
        /// 1：求交集
        /// 2：求差集
        /// </param>
        /// <param name="keys">key列表</param>
        /// <returns></returns>
        List<RedisValue> Combine(SetOperation operation, string[] keys);

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <param name="operation">
        /// 0：求并集
        /// 1：求交集
        /// 2：求差集
        /// </param>
        /// <param name="keys">key列表</param>
        /// <returns></returns>
        List<T> Combine<T>(SetOperation operation, string[] keys) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destinationKey"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <returns></returns>
        long CombineAndStore(SetOperation operation, string destinationKey, string firstKey, string secondKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destinationKey"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        long CombineAndStore(SetOperation operation, string destinationKey, string[] keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Contains(string key, RedisValue value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Contains<T>(string key, T value) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long Length(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<RedisValue> SetMembers(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<T> SetMembers<T>(string key) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Move(string source, string destination, RedisValue value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Move<T>(string source, string destination, T value) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        RedisValue Pop(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T Pop<T>(string key) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        RedisValue RandomMember(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T RandomMember<T>(string key) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<RedisValue> RandomMembers(string key, long count);

        /// <summary>
        /// 删除指定的列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Remove(string key, RedisValue value);

        /// <summary>
        /// 删除指定的列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Remove<T>(string key, T value) where T : IModel;

        /// <summary>
        /// 删除指定的列表项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        long Remove(string key, params RedisValue[] values);

        /// <summary>
        /// 删除指定的列表项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        long Remove<T>(string key, params T[] values) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<RedisValue> Scan(string key, RedisValue pattern, int pageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<T> Scan<T>(string key, T pattern, int pageSize) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="cursor"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        IEnumerable<RedisValue> Scan(string key, RedisValue pattern = default(RedisValue), int pageSize = 10, long cursor = 0, int pageOffset = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="cursor"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        IEnumerable<T> Scan<T>(string key, T pattern = default(T), int pageSize = 10, long cursor = 0, int pageOffset = 0) where T : IModel;

        /// <summary>
        /// 设置集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, RedisValue value);

        /// <summary>
        /// 设置集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> SetAsync<T>(string key, T value) where T : IModel;

        /// <summary>
        /// 批量设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<long> SetAsync(string key, params RedisValue[] values);

        /// <summary>
        /// 批量设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<long> SetAsync<T>(string key, params T[] values) where T : IModel;

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <param name="operation">
        /// 0：求并集
        /// 1：求交集
        /// 2：求差集
        /// </param>
        /// <param name="firstKey">第一个Key</param>
        /// <param name="secondKey">第二个Key</param>
        /// <returns></returns>
        Task<List<RedisValue>> CombineAsync(SetOperation operation, string firstKey, string secondKey);

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <param name="operation">
        /// 0：求并集
        /// 1：求交集
        /// 2：求差集
        /// </param>
        /// <param name="firstKey">第一个Key</param>
        /// <param name="secondKey">第二个Key</param>
        /// <returns></returns>
        Task<List<T>> CombineAsync<T>(SetOperation operation, string firstKey, string secondKey) where T : IModel;

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <param name="operation">
        /// 0：求并集
        /// 1：求交集
        /// 2：求差集
        /// </param>
        /// <param name="keys">key列表</param>
        /// <returns></returns>
        Task<List<RedisValue>> CombineAsync(SetOperation operation, string[] keys);

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <param name="operation">
        /// 0：求并集
        /// 1：求交集
        /// 2：求差集
        /// </param>
        /// <param name="keys">key列表</param>
        /// <returns></returns>
        Task<List<T>> CombineAsync<T>(SetOperation operation, string[] keys) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destinationKey"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <returns></returns>
        Task<long> CombineAndStoreAsync(SetOperation operation, string destinationKey, string firstKey, string secondKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destinationKey"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<long> CombineAndStoreAsync(SetOperation operation, string destinationKey, string[] keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> ContainsAsync(string key, RedisValue value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> ContainsAsync<T>(string key, T value) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> LengthAsync(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<RedisValue>> SetMembersAsync(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<T>> SetMembersAsync<T>(string key) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> MoveAsync(string source, string destination, RedisValue value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> MoveAsync<T>(string source, string destination, T value) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValue> PopAsync(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> PopAsync<T>(string key) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValue> RandomMemberAsync(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> RandomMemberAsync<T>(string key) where T : IModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<RedisValue>> RandomMembersAsync(string key, long count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<T>> RandomMembersAsync<T>(string key, long count) where T : IModel;

        /// <summary>
        /// 删除指定的列表项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<long> RemoveAsync(string key, params RedisValue[] values);

        /// <summary>
        /// 删除指定的列表项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<long> RemoveAsync<T>(string key, params T[] values) where T : IModel;

        /// <summary>
        /// 删除指定的列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key, RedisValue value);

        /// <summary>
        /// 删除指定的列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync<T>(string key, T value) where T : IModel;
    }
}