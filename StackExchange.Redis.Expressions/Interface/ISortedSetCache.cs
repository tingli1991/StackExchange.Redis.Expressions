using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Interface
{
    /// <summary>
    /// 有序集合缓存
    /// </summary>
    public interface ISortedSetCache : ICache
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        bool Set(string key, RedisValue member, double score);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        bool Set(string key, RedisValue member, double score, When when = When.Always);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        long Set(string key, SortedSetEntry[] values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        long Set(string key, SortedSetEntry[] values, When when = When.Always);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destination"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        long CombineAndStore(SetOperation operation, string destination, string first, string second, Aggregate aggregate = Aggregate.Sum);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destination"></param>
        /// <param name="keys"></param>
        /// <param name="weights"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        long CombineAndStore(SetOperation operation, string destination, string[] keys, double[] weights = null, Aggregate aggregate = Aggregate.Sum);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        double Decrement(string key, RedisValue member, double value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        double Increment(string key, RedisValue member, double value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        long Length(RedisKey key, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        long LengthByValue(string key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        List<RedisValue> RangeByRank(string key, long start = 0, long stop = -1, Order order = Order.Ascending);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        List<SortedSetEntry> RangeByRankWithScores(string key, long start = 0, long stop = -1, Order order = Order.Ascending);

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
        List<RedisValue> RangeByScore(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1);

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
        List<SortedSetEntry> RangeByScoreWithScores(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1);

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
        List<RedisValue> RangeByValue(string key, RedisValue min = default(RedisValue), RedisValue max = default(RedisValue), Exclude exclude = Exclude.None, long skip = 0, long take = -1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        long? Rank(string key, RedisValue member, Order order = Order.Ascending);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        bool Remove(string key, RedisValue member);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        long Remove(string key, RedisValue[] members);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        long RemoveRangeByRank(string key, long start, long stop);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        long RemoveRangeByScore(string key, double start, double stop, Exclude exclude = Exclude.None);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        long RemoveRangeByValue(string key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<SortedSetEntry> Scan(string key, RedisValue pattern, int pageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="cursor"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        IEnumerable<SortedSetEntry> Scan(string key, RedisValue pattern = default(RedisValue), int pageSize = 10, long cursor = 0, int pageOffset = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        double? Score(string key, RedisValue member);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, RedisValue member, double score);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, RedisValue member, double score, When when = When.Always);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<long> SetAddAsync(string key, SortedSetEntry[] values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<long> SetAsync(string key, SortedSetEntry[] values, When when = When.Always);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destination"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        Task<long> CombineAndStoreAsync(SetOperation operation, string destination, string first, string second, Aggregate aggregate = Aggregate.Sum);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destination"></param>
        /// <param name="keys"></param>
        /// <param name="weights"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        Task<long> CombineAndStoreAsync(SetOperation operation, string destination, string[] keys, double[] weights = null, Aggregate aggregate = Aggregate.Sum);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<double> DecrementAsync(string key, RedisValue member, double value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<double> IncrementAsync(string key, RedisValue member, double value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        Task<long> LengthAsync(RedisKey key, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        Task<long> LengthByValueAsync(string key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<List<RedisValue>> RangeByRankAsync(string key, long start = 0, long stop = -1, Order order = Order.Ascending);

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
        Task<List<RedisValue>> RangeByScoreAsync(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1);

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
        Task<List<SortedSetEntry>> RangeByScoreWithScoresAsync(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1);

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
        Task<List<RedisValue>> RangeByValueAsync(string key, RedisValue min = default(RedisValue), RedisValue max = default(RedisValue), Exclude exclude = Exclude.None, long skip = 0, long take = -1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<long?> RankAsync(string key, RedisValue member, Order order = Order.Ascending);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key, RedisValue member);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        Task<long> RemoveAsync(string key, RedisValue[] members);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<long> RemoveRangeByRankAsync(string key, long start, long stop);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        Task<long> RemoveRangeByScoreAsync(string key, double start, double stop, Exclude exclude = Exclude.None);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        Task<long> RemoveRangeByValueAsync(string key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<double?> ScoreAsync(string key, RedisValue member);
    }
}