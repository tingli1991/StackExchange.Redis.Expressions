using StackExchange.Redis.Expressions.Interface;
using StackExchange.Redis.Expressions.Serialize;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Clients
{
    /// <summary>
    /// 结合缓存客户端
    /// </summary>
    public class SetCacheClient : CacheClient, ISetCache
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public SetCacheClient() : base()
        {

        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="dbIndex"></param>
        public SetCacheClient(int dbIndex = 0) : base(dbIndex)
        {

        }

        /// <summary>
        /// 设置集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, RedisValue value)
        {
            return Execute(key, (newKey, db) => db.SetAdd(newKey, value));
        }

        /// <summary>
        /// 设置集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T value) where T : IModel
        {
            return Execute(key, (newKey, db) => db.SetAdd(newKey, value.Serialize()));
        }

        /// <summary>
        /// 批量设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public long Set(string key, params RedisValue[] values)
        {
            return Execute(key, (newKey, db) => db.SetAdd(newKey, values));
        }

        /// <summary>
        /// 批量设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public long Set<T>(string key, params T[] values) where T : IModel
        {
            return Execute(key, (newKey, db) => db.SetAdd(newKey, values.Select(e => (RedisValue)e.Serialize()).ToArray()));
        }

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
        public List<RedisValue> Combine(SetOperation operation, string firstKey, string secondKey)
        {
            return _db.SetCombine(operation, MergeKey(firstKey), MergeKey(secondKey)).ToList();
        }

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
        public List<T> Combine<T>(SetOperation operation, string firstKey, string secondKey) where T : IModel
        {
            var values = _db.SetCombine(operation, MergeKey(firstKey), MergeKey(secondKey));
            return values.Select(e => e.DeSerialize<T>()).ToList();
        }

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
        public List<RedisValue> Combine(SetOperation operation, string[] keys)
        {
            var keyList = keys.Select(e => (RedisKey)MergeKey(e)).ToArray();
            return _db.SetCombine(operation, keyList).ToList();
        }

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
        public List<T> Combine<T>(SetOperation operation, string[] keys) where T : IModel
        {
            var keyList = keys.Select(e => (RedisKey)MergeKey(e)).ToArray();
            return _db.SetCombine(operation, keyList).Select(e => e.DeSerialize<T>()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destinationKey"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <returns></returns>
        public long CombineAndStore(SetOperation operation, string destinationKey, string firstKey, string secondKey)
        {
            return _db.SetCombineAndStore(operation, MergeKey(destinationKey), MergeKey(firstKey), MergeKey(secondKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destinationKey"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long CombineAndStore(SetOperation operation, string destinationKey, string[] keys)
        {
            var keyList = keys.Select(e => (RedisKey)MergeKey(e)).ToArray();
            return _db.SetCombineAndStore(operation, MergeKey(destinationKey), keyList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(string key, RedisValue value)
        {
            return Execute(key, (newKey, db) => db.SetContains(newKey, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains<T>(string key, T value) where T : IModel
        {
            return Execute(key, (newKey, db) => db.SetContains(newKey, value.Serialize()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long Length(string key)
        {
            return Execute(key, (newKey, db) => db.SetLength(newKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<RedisValue> SetMembers(string key)
        {
            return Execute(key, (newKey, db) => db.SetMembers(newKey)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> SetMembers<T>(string key) where T : IModel
        {
            return Execute(key, (newKey, db) => db.SetMembers(newKey).Select(e => e.DeSerialize<T>()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Move(string source, string destination, RedisValue value)
        {
            return _db.SetMove(MergeKey(source), MergeKey(destination), value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Move<T>(string source, string destination, T value) where T : IModel
        {
            return _db.SetMove(MergeKey(source), MergeKey(destination), value.Serialize());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedisValue Pop(string key)
        {
            return Execute(key, (newKey, db) => db.SetPop(newKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Pop<T>(string key) where T : IModel
        {
            return Execute(key, (newKey, db) => db.SetPop(newKey).DeSerialize<T>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedisValue RandomMember(string key)
        {
            return Execute(key, (newKey, db) => db.SetRandomMember(newKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T RandomMember<T>(string key) where T : IModel
        {
            return Execute(key, (newKey, db) => db.SetRandomMember(newKey).DeSerialize<T>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<RedisValue> RandomMembers(string key, long count)
        {
            return Execute(key, (newKey, db) => db.SetRandomMembers(newKey, count).ToList());
        }

        /// <summary>
        /// 删除指定的列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(string key, RedisValue value)
        {
            return Execute(key, (newKey, db) => db.SetRemove(newKey, value));
        }

        /// <summary>
        /// 删除指定的列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove<T>(string key, T value) where T : IModel
        {
            return Execute(key, (newKey, db) => db.SetRemove(newKey, value.Serialize()));
        }

        /// <summary>
        /// 删除指定的列表项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public long Remove(string key, params RedisValue[] values)
        {
            return Execute(key, (newKey, db) => db.SetRemove(newKey, values));
        }

        /// <summary>
        /// 删除指定的列表项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public long Remove<T>(string key, params T[] values) where T : IModel
        {
            return Execute(key, (newKey, db) => db.SetRemove(newKey, values.Select(e => (RedisValue)e.Serialize()).ToArray()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<RedisValue> Scan(string key, RedisValue pattern, int pageSize)
        {
            return Execute(key, (newKey, db) => db.SetScan(newKey, pattern, pageSize));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<T> Scan<T>(string key, T pattern, int pageSize) where T : IModel
        {
            return Execute(key, (newKey, db) => db.SetScan(newKey, pattern.Serialize(), pageSize).Select(e => e.DeSerialize<T>()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="cursor"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        public IEnumerable<RedisValue> Scan(string key, RedisValue pattern = default(RedisValue), int pageSize = 10, long cursor = 0, int pageOffset = 0)
        {
            return Execute(key, (newKey, db) => db.SetScan(newKey, pattern, pageSize, cursor, pageOffset));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="cursor"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        public IEnumerable<T> Scan<T>(string key, T pattern = default(T), int pageSize = 10, long cursor = 0, int pageOffset = 0) where T : IModel
        {
            return Execute(key, (newKey, db) =>
            {
                var values = db.SetScan(newKey, pattern.Serialize(), pageSize, cursor, pageOffset);
                return values.Select(e => e.DeSerialize<T>()).ToList();
            });
        }

        /// <summary>
        /// 设置集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, RedisValue value)
        {
            return await Execute(key, (newKey, db) => db.SetAddAsync(newKey, value));
        }

        /// <summary>
        /// 设置集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, T value) where T : IModel
        {
            return await Execute(key, (newKey, db) => db.SetAddAsync(newKey, value.Serialize()));
        }

        /// <summary>
        /// 批量设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<long> SetAsync(string key, params RedisValue[] values)
        {
            return await Execute(key, (newKey, db) => db.SetAddAsync(newKey, values));
        }

        /// <summary>
        /// 批量设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<long> SetAsync<T>(string key, params T[] values) where T : IModel
        {
            return await Execute(key, (newKey, db) => db.SetAddAsync(newKey, values.Select(e => (RedisValue)e.Serialize()).ToArray()));
        }

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
        public async Task<List<RedisValue>> CombineAsync(SetOperation operation, string firstKey, string secondKey)
        {
            var values = await _db.SetCombineAsync(operation, MergeKey(firstKey), MergeKey(secondKey));
            return values.ToList();
        }

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
        public async Task<List<T>> CombineAsync<T>(SetOperation operation, string firstKey, string secondKey) where T : IModel
        {
            var values = await _db.SetCombineAsync(operation, MergeKey(firstKey), MergeKey(secondKey));
            return values.Select(e => e.DeSerialize<T>()).ToList();
        }

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
        public async Task<List<RedisValue>> CombineAsync(SetOperation operation, string[] keys)
        {
            var values = await _db.SetCombineAsync(operation, keys.Select(e => (RedisKey)MergeKey(e)).ToArray());
            return values.ToList();
        }

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
        public async Task<List<T>> CombineAsync<T>(SetOperation operation, string[] keys) where T : IModel
        {
            var values = await _db.SetCombineAsync(operation, keys.Select(e => (RedisKey)MergeKey(e)).ToArray());
            return values.Select(e => e.DeSerialize<T>()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destinationKey"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <returns></returns>
        public async Task<long> CombineAndStoreAsync(SetOperation operation, string destinationKey, string firstKey, string secondKey)
        {
            return await _db.SetCombineAndStoreAsync(operation, MergeKey(destinationKey), MergeKey(firstKey), MergeKey(secondKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destinationKey"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<long> CombineAndStoreAsync(SetOperation operation, string destinationKey, string[] keys)
        {
            var keyArray = keys.Select(e => (RedisKey)MergeKey(e)).ToArray();
            return await _db.SetCombineAndStoreAsync(operation, MergeKey(destinationKey), keys.Select(e => (RedisKey)MergeKey(e)).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> ContainsAsync(string key, RedisValue value)
        {
            return await Execute(key, (newKey, db) => db.SetContainsAsync(newKey, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> ContainsAsync<T>(string key, T value) where T : IModel
        {
            return await Execute(key, (newKey, db) => db.SetContainsAsync(newKey, value.Serialize()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> LengthAsync(string key)
        {
            return await Execute(key, (newKey, db) => db.SetLengthAsync(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<RedisValue>> SetMembersAsync(string key)
        {
            var values = await Execute(key, (newKey, db) => db.SetMembersAsync(newKey));
            return values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> SetMembersAsync<T>(string key) where T : IModel
        {
            var values = await Execute(key, (newKey, db) => db.SetMembersAsync(newKey));
            return values.Select(e => e.DeSerialize<T>()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> MoveAsync(string source, string destination, RedisValue value)
        {
            return await _db.SetMoveAsync(MergeKey(source), MergeKey(destination), value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> MoveAsync<T>(string source, string destination, T value) where T : IModel
        {
            return await _db.SetMoveAsync(MergeKey(source), MergeKey(destination), value.Serialize());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<RedisValue> PopAsync(string key)
        {
            return await Execute(key, (newKey, db) => db.SetPopAsync(newKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> PopAsync<T>(string key) where T : IModel
        {
            var value = await Execute(key, (newKey, db) => db.SetPopAsync(newKey));
            return value.DeSerialize<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<RedisValue> RandomMemberAsync(string key)
        {
            return await Execute(key, (newKey, db) => db.SetRandomMemberAsync(newKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> RandomMemberAsync<T>(string key) where T : IModel
        {
            var value = await Execute(key, (newKey, db) => db.SetRandomMemberAsync(newKey));
            return value.DeSerialize<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<RedisValue>> RandomMembersAsync(string key, long count)
        {
            var values = await Execute(key, (newKey, db) => db.SetRandomMembersAsync(newKey, count));
            return values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<T>> RandomMembersAsync<T>(string key, long count) where T : IModel
        {
            var values = await Execute(key, (newKey, db) => db.SetRandomMembersAsync(newKey, count));
            return values.Select(e => e.DeSerialize<T>()).ToList();
        }

        /// <summary>
        /// 删除指定的列表项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<long> RemoveAsync(string key, params RedisValue[] values)
        {
            return await Execute(key, (newKey, db) => db.SetRemoveAsync(newKey, values));
        }

        /// <summary>
        /// 删除指定的列表项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<long> RemoveAsync<T>(string key, params T[] values) where T : IModel
        {
            return await Execute(key, (newKey, db) => db.SetRemoveAsync(newKey, values.Select(e => (RedisValue)e.Serialize()).ToArray()));
        }

        /// <summary>
        /// 删除指定的列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key, RedisValue value)
        {
            return await Execute(key, (newKey, db) => db.SetRemoveAsync(newKey, value));
        }

        /// <summary>
        /// 删除指定的列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync<T>(string key, T value) where T : IModel
        {
            return await Execute(key, (newKey, db) => db.SetRemoveAsync(newKey, value.Serialize()));
        }
    }
}