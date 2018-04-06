using System;
using System.Threading.Tasks;

namespace StackExchange.Redis.Expressions.Interface
{
    /// <summary>
    /// 缓存处理基础对象
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// 设置到期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool SetExpiryTime(string key, TimeSpan expiry);

        /// <summary>
        /// 设置到期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool SetExpiryTime(string key, DateTime expiry);

        /// <summary>
        ///  键重命名
        /// </summary>
        /// <param name="oldKey">旧值</param>
        /// <param name="newKey">新值</param>
        /// <returns></returns>
        bool RenameKey(string oldKey, string newKey);

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key);

        /// <summary>
        /// 设置到期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> SetExpiryTimeAsync(string key, DateTime expiry);

        /// <summary>
        /// 设置到期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> SetExpiryTimeAsync(string key, TimeSpan expiry);

        /// <summary>
        ///  键重命名
        /// </summary>
        /// <param name="oldKey">旧值</param>
        /// <param name="newKey">新值</param>
        /// <returns></returns>
        Task<bool> RenameKeyAsync(string oldKey, string newKey);
    }
}