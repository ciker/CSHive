using System;
using CS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CS
{
    [TestClass]
    public class SRTests
    {

        [TestMethod]
        public void GetStringTest()
        {
            var val = SR.GetString("Demo_Format", new[] {"1","2"}); //注意，在Template中的字符串点位符必须小于等于传入的参数数量
            Console.WriteLine(val);
            Assert.IsTrue(val == "1--2");
        }
    }
}