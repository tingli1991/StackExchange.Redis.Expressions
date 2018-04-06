using StackExchange.Redis.Expressions.Interface;
using System;
using System.Runtime.Serialization;

namespace StackExchange.Redis.Expressions.UnitTest.Models
{
    /// <summary>
    /// 学员信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class StudentModel : IModel
    {
        /// <summary>
        /// 学员Id
        /// </summary>
        [DataMember(Order = 1)]
        public int Id { get; set; }

        /// <summary>
        /// 学员名称
        /// </summary>
        [DataMember(Order = 2)]
        public string Name { get; set; }
    }
}