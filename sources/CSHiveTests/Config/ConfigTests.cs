using System;
using System.Security.Cryptography.X509Certificates;
using CS.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CS.Config
{

    [TestClass]
    public class ConfigTests
    {

        [TestMethod]
        public void CDATATest()
        {
            var cfg = new ConfigHelper();
            //Console.WriteLine(cfg.DemoString);
            Assert.IsNotNull(cfg.DemoString);

            //Console.WriteLine(cfg.Demo2);
            Assert.IsTrue(cfg.Demo2.Length > 10);

            //Console.WriteLine(cfg.Demo3);
            Assert.IsTrue(cfg.Demo3 == "demo3");
        }
    }




    public class ConfigHelper : SectionBase
    {
        public ConfigHelper() : base("KVDemo")
        {
        }

        public string DemoString => KeyValues["demo"];

        public string Demo2 => KeyValues["demo2"];

        public string Demo3 => KeyValues["demo3"];

    }
}