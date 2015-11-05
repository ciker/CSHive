using System;
using System.Globalization;
using System.Security.Cryptography;
using CS.Caching;
using CS.Cryptography;
using CS.Diagnostics;
using NUnit.Framework;

namespace CSHive.Tests.Caching
{
    
    [TestFixture]
    public class FuncCacheTests
    {
        [Test]
        public void FuncCacheItemTest()
        {
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(MockService.Token);
            }
            Assert.True(1==1);
        }


        
    }

    internal class AppItem
    {
        public string Token { get; set; }

        public int Expiress { get; set; }
    }

    internal class MockService
    {
        private static readonly TestCacheItem cacheItem;  
        static MockService()
        {
            cacheItem = new TestCacheItem();
        }

        public static string Token => cacheItem.Item.Token;

        //public static 

        static Random rnd = new Random();

        internal static Json GetMockJson()
        {
            var json = new Json();
            json.ExpiresIn = 2700;
            json.Token = Md5.Encrypt(rnd.NextDouble().ToString(CultureInfo.InvariantCulture));
            return json;
        }
    }

    class TestCacheItem:FuncCacheItem<Json>
    {
        public TestCacheItem() : base(MockService.GetMockJson, null)
        {
        }
        protected override void UpdateExpires(Json json)
        {
            Tracer.Debug(json.Token);
            SetExpiresTime(json.ExpiresIn);
        }
    }


    class Json
    {
        public string Token { get; set; }

        public int ExpiresIn { get; set; }
    }
}