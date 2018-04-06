using Jil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis.Expressions.Clients;
using StackExchange.Redis.Expressions.Interface;
using StackExchange.Redis.Expressions.UnitTest.Models;
using System;

namespace StackExchange.Redis.Expressions.UnitTest
{
    [TestClass]
    public class HashCacheClientUnitTest
    {
        [TestMethod]
        public void TestSetToJil()
        {
            //设置key
            var key = "HashTest";
            var hashClient = new HashCacheClient();
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                //设置缓存
                var student = new StudentModel() { Id = i, Name = $"Tingli_No{i}" } as IModel;
                hashClient.Set(key, $"{i}", student);

                //获取缓存
                var model = hashClient.Get<StudentModel>(key, $"{i}");
            }
            stopwatch.Stop();
            var totalSeconds = stopwatch.Elapsed.TotalSeconds;
        }
    }
}
