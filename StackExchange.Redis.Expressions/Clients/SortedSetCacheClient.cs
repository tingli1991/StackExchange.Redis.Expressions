using StackExchange.Redis.Expressions.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Clients
{
    /// <summary>
    /// 有序队列缓存客户端
    /// </summary>
    public class SortedSetCacheClient : CacheClient, ISortedSetCache
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool Set(string key, RedisValue member, double score)
        {
            return Execute(key, (newKey, db) => db.SortedSetAdd(newKey, member, score));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public bool Set(string key, RedisValue member, double score, When when = When.Always)
        {
            return Execute(key, (newKey, db) => db.SortedSetAdd(newKey, member, score, when));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public long Set(string key, SortedSetEntry[] values)
        {
            return Execute(key, (newKey, db) => db.SortedSetAdd(newKey, values));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public long Set(string key, SortedSetEntry[] values, When when = When.Always)
        {
            return Execute(key, (newKey, db) => db.SortedSetAdd(newKey, values, when));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destination"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public long CombineAndStore(SetOperation operation, string destination, string first, string second, Aggregate aggregate = Aggregate.Sum)
        {
            return _db.SortedSetCombineAndStore(operation, MergeKey(destination), MergeKey(first), MergeKey(second), aggregate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destination"></param>
        /// <param name="keys"></param>
        /// <param name="weights"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public long CombineAndStore(SetOperation operation, string destination, string[] keys, double[] weights = null, Aggregate aggregate = Aggregate.Sum)
        {
            return _db.SortedSetCombineAndStore(operation, MergeKey(destination), keys.Select(e => (RedisKey)MergeKey(e)).ToArray(), weights, aggregate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Decrement(string key, RedisValue member, double value)
        {
            return Execute(key, (newKey, db) => db.SortedSetDecrement(newKey, member, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Increment(string key, RedisValue member, double value)
        {
            return Execute(key, (newKey, db) => db.SortedSetIncrement(newKey, member, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public long Length(RedisKey key, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None)
        {
            return Execute(key, (newKey, db) => db.SortedSetLength(newKey, min, max, exclude));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public long LengthByValue(string key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None)
        {
            return Execute(key, (newKey, db) => db.SortedSetLengthByValue(newKey, min, max, exclude));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<RedisValue> RangeByRank(string key, long start = 0, long stop = -1, Order order = Order.Ascending)
        {
            return Execute(key, (newKey, db) => db.SortedSetRangeByRank(newKey, start, stop, order).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<SortedSetEntry> RangeByRankWithScores(string key, long start = 0, long stop = -1, Order order = Order.Ascending)
        {
            return Execute(key, (newKey, db) => db.SortedSetRangeByRankWithScores(newKey, start, stop, order).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="exclude"></param>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<RedisValue> RangeByScore(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1)
        {
            return Execute(key, (newKey, db) => db.SortedSetRangeByScore(newKey, start, stop, exclude, order).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="exclude"></param>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<SortedSetEntry> RangeByScoreWithScores(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1)
        {
            return Execute(key, (newKey, db) => db.SortedSetRangeByScoreWithScores(newKey, start, stop, exclude, order).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<RedisValue> RangeByValue(string key, RedisValue min = default(RedisValue), RedisValue max = default(RedisValue), Exclude exclude = Exclude.None, long skip = 0, long take = -1)
        {
            return Execute(key, (newKey, db) => db.SortedSetRangeByValue(newKey, min, max, exclude, skip, take).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public long? Rank(string key, RedisValue member, Order order = Order.Ascending)
        {
            return Execute(key, (newKey, db) => db.SortedSetRank(newKey, member, order));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Remove(string key, RedisValue member)
        {
            return Execute(key, (newKey, db) => db.SortedSetRemove(newKey, member));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public long Remove(string key, RedisValue[] members)
        {
            return Execute(key, (newKey, db) => db.SortedSetRemove(newKey, members));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public long RemoveRangeByRank(string key, long start, long stop)
        {
            return Execute(key, (newKey, db) => db.SortedSetRemoveRangeByRank(newKey, start, stop));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public long RemoveRangeByScore(string key, double start, double stop, Exclude exclude = Exclude.None)
        {
            return Execute(key, (newKey, db) => db.SortedSetRemoveRangeByScore(newKey, start, stop, exclude));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public long RemoveRangeByValue(string key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None)
        {
            return Execute(key, (newKey, db) => db.SortedSetRemoveRangeByValue(newKey, min, max, exclude));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<SortedSetEntry> Scan(string key, RedisValue pattern, int pageSize)
        {
            return Execute(key, (newKey, db) => db.SortedSetScan(newKey, pattern, pageSize));
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
        public IEnumerable<SortedSetEntry> Scan(string key, RedisValue pattern = default(RedisValue), int pageSize = 10, long cursor = 0, int pageOffset = 0)
        {
            return Execute(key, (newKey, db) => db.SortedSetScan(newKey, pattern, pageSize, cursor, pageOffset));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public double? Score(string key, RedisValue member)
        {
            return Execute(key, (newKey, db) => db.SortedSetScore(newKey, member));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, RedisValue member, double score)
        {
            return await Execute(key, (newKey, db) => db.SortedSetAddAsync(newKey, member, score));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, RedisValue member, double score, When when = When.Always)
        {
            return await Execute(key, (newKey, db) => db.SortedSetAddAsync(newKey, member, score, when));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<long> SetAddAsync(string key, SortedSetEntry[] values)
        {
            return await Execute(key, (newKey, db) => db.SortedSetAddAsync(newKey, values));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<long> SetAsync(string key, SortedSetEntry[] values, When when = When.Always)
        {
            return await Execute(key, (newKey, db) => db.SortedSetAddAsync(newKey, values, when));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destination"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public async Task<long> CombineAndStoreAsync(SetOperation operation, string destination, string first, string second, Aggregate aggregate = Aggregate.Sum)
        {
            return await _db.SortedSetCombineAndStoreAsync(operation, MergeKey(destination), MergeKey(first), MergeKey(second), aggregate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destination"></param>
        /// <param name="keys"></param>
        /// <param name="weights"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public async Task<long> CombineAndStoreAsync(SetOperation operation, string destination, string[] keys, double[] weights = null, Aggregate aggregate = Aggregate.Sum)
        {
            return await _db.SortedSetCombineAndStoreAsync(operation, destination, keys.Select(e => (RedisKey)MergeKey(e)).ToArray(), weights, aggregate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<double> DecrementAsync(string key, RedisValue member, double value)
        {
            return await Execute(key, (newKey, db) => db.SortedSetDecrementAsync(newKey, member, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<double> IncrementAsync(string key, RedisValue member, double value)
        {
            return await Execute(key, (newKey, db) => db.SortedSetIncrementAsync(newKey, member, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public async Task<long> LengthAsync(RedisKey key, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None)
        {
            return await Execute(key, (newKey, db) => db.SortedSetLengthAsync(newKey, min, max, exclude));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public async Task<long> LengthByValueAsync(string key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None)
        {
            return await Execute(key, (newKey, db) => db.SortedSetLengthByValueAsync(newKey, min, max, exclude));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<List<RedisValue>> RangeByRankAsync(string key, long start = 0, long stop = -1, Order order = Order.Ascending)
        {
            var values = await Execute(key, (newKey, db) => db.SortedSetRangeByRankAsync(newKey, start, stop, order));
            return values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="exclude"></param>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<RedisValue>> RangeByScoreAsync(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1)
        {
            var values = await Execute(key, (newKey, db) => db.SortedSetRangeByScoreAsync(newKey, start, stop, exclude, order));
            return values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="exclude"></param>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<SortedSetEntry>> RangeByScoreWithScoresAsync(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1)
        {
            var values = await Execute(key, (newKey, db) => db.SortedSetRangeByScoreWithScoresAsync(newKey, start, stop, exclude, order, skip, take));
            return values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<RedisValue>> RangeByValueAsync(string key, RedisValue min = default(RedisValue), RedisValue max = default(RedisValue), Exclude exclude = Exclude.None, long skip = 0, long take = -1)
        {
            var values = await Execute(key, (newKey, db) => db.SortedSetRangeByValueAsync(newKey, min, max, exclude, skip, take));
            return values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<long?> RankAsync(string key, RedisValue member, Order order = Order.Ascending)
        {
            return await Execute(key, (newKey, db) => db.SortedSetRankAsync(newKey, member, order));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key, RedisValue member)
        {
            return await Execute(key, (newKey, db) => db.SortedSetRemoveAsync(newKey, member));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public async Task<long> RemoveAsync(string key, RedisValue[] members)
        {
            return await Execute(key, (newKey, db) => db.SortedSetRemoveAsync(newKey, members));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<long> RemoveRangeByRankAsync(string key, long start, long stop)
        {
            return await Execute(key, (newKey, db) => db.SortedSetRemoveRangeByRankAsync(newKey, start, stop));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public async Task<long> RemoveRangeByScoreAsync(string key, double start, double stop, Exclude exclude = Exclude.None)
        {
            return await Execute(key, (newKey, db) => db.SortedSetRemoveRangeByScoreAsync(newKey, start, stop, exclude));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public async Task<long> RemoveRangeByValueAsync(string key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None)
        {
            return await Execute(key, (newKey, db) => db.SortedSetRemoveRangeByValueAsync(newKey, min, max, exclude));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<double?> ScoreAsync(string key, RedisValue member)
        {
            return await Execute(key, (newKey, db) => db.SortedSetScoreAsync(newKey, member));
        }
    }
}