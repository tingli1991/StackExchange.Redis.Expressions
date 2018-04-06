using StackExchange.Redis.Expressions.Interface;
using System;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Clients
{
    /// <summary>
    /// 缓存客户端基础处理业务
    /// </summary>
    public class CacheClient : BaseClient, ICache
    {
        /// <summary>
        /// 数据存放的db实例
        /// </summary>
        protected readonly int _dbIndex = 0;

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        protected readonly IDatabase _db = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CacheClient() : this(0)
        {

        }

        /// <summary>
        /// 初始数据库
        /// </summary>
        /// <param name="dbIndex"></param>
        public CacheClient(int dbIndex) : base()
        {
            _dbIndex = dbIndex;
            _db = _instance.GetDatabase(_dbIndex);
        }
        
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected T Execute<T>(string key, Func<string, IDatabase, T> func)
        {
            key = MergeKey(key);
            return func(key, _db);
        }

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return Execute(key, (newKey, db) => db.KeyExists(newKey));
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Execute(key, (newKey, db) => db.KeyDelete(newKey));
        }

        /// <summary>
        /// 设置到期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool SetExpiryTime(string key, TimeSpan expiry)
        {
            return Execute(key, (newKey, db) => db.KeyExpire(newKey, expiry));
        }

        /// <summary>
        ///  键重命名
        /// </summary>
        /// <param name="oldKey">旧值</param>
        /// <param name="newKey">新值</param>
        /// <returns></returns>
        public bool RenameKey(string oldKey, string newKey)
        {
            return Execute(oldKey, (key, db) => db.KeyRename(key, MergeKey(newKey)));
        }

        /// <summary>
        /// 设置到期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool SetExpiryTime(string key, DateTime expiry)
        {
            return Execute(key, (newKey, db) => db.KeyExpire(newKey, expiry));
        }

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key)
        {
            return await Execute(key, (newKey, db) => db.KeyExistsAsync(newKey));
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key)
        {
            return await Execute(key, (newKey, db) => db.KeyDeleteAsync(newKey));
        }

        /// <summary>
        /// 设置到期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> SetExpiryTimeAsync(string key, DateTime expiry)
        {
            return await Execute(key, (newKey, db) => db.KeyExpireAsync(newKey, expiry));
        }

        /// <summary>
        /// 设置到期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> SetExpiryTimeAsync(string key, TimeSpan expiry)
        {
            return await Execute(key, (newKey, db) => db.KeyExpireAsync(newKey, expiry));
        }

        /// <summary>
        ///  键重命名
        /// </summary>
        /// <param name="oldKey">旧值</param>
        /// <param name="newKey">新值</param>
        /// <returns></returns>
        public async Task<bool> RenameKeyAsync(string oldKey, string newKey)
        {
            return await Execute(oldKey, (key, db) => db.KeyRenameAsync(key, MergeKey(newKey)));
        }
    }
}