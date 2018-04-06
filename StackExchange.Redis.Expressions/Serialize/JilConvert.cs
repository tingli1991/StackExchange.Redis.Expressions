using Jil;
using StackExchange.Redis.Expressions.Interface;

namespace StackExchange.Redis.Expressions.Serialize
{
    /// <summary>
    /// Protobuf序列化扩展类
    /// </summary>
    public static class JilConvert
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static string Serialize(this IModel sender)
        {
            return JSON.SerializeDynamic(sender);
        }

        /// <summary>  
        /// 反序列化  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="value"></param>  
        /// <returns></returns>  
        public static T DeSerialize<T>(this RedisValue value) where T : IModel
        {
            return JSON.Deserialize<T>(value);
        }
    }
}