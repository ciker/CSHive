using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Tests
{
    [TestClass()]
    public class XmlExtensionTests
    {
        [TestMethod()]
        public void ToXmlTest()
        {
            var o = new {x1 = "test", age = 20};
            //Console.WriteLine(o.ToXml2());
        }
    }
}