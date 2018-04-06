using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis.Expressions.Clients;
using StackExchange.Redis.Expressions.Interface;
using StackExchange.Redis.Expressions.UnitTest.Models;
using System;

namespace StackExchange.Redis.Expressions.UnitTest
{
    [TestClass]
    public class StringCacheClientUnitTest
    {
        [TestMethod]
        public void TestSetToJil()
        {
            //设置key
            var key = "StringsTest";
            var strClient = new StringCacheClient();
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                var newKey = $"{key}_{i}";

                //设置缓存
                var student = new StudentModel() { Id = i, Name = $"Tingli:No.{i}" } as IModel;
                strClient.Set(newKey, student, DateTime.Now.AddDays(1));

                //获取缓存
                var model = strClient.Get<StudentModel>(newKey);
            }
            stopwatch.Stop();
            var totalSeconds = stopwatch.Elapsed.TotalSeconds;
        }
    }
}