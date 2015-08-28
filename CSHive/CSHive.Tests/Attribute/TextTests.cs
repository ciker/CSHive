using System.Reflection;
using CS.Attribute;
using CS.Diagnostics;
using NUnit.Framework;

namespace CSHive.Tests.Attribute
{
    [TestFixture]
    public class TextTests
    {
        [Test]
        public void Test1()
        {
            var tp = typeof (User);
           
           // var fs1 = tp.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var fs = tp.GetProperties();
            foreach (var info in fs)
            {
                var ca2 = info.GetCustomAttributes(typeof (TextAttribute), false);
                var ca = info.GetCustomAttributesData();
                Tracer.Debug(ca);
            }



            Assert.IsTrue(1==1);
        }

    }

    public class User
    {
        [Text("用户名")]
        public string Name { get; set; }

        [Text("Id")]
        public int Id { get; set; }

        [Text("实名")]
        public string Realname { get; set; }

        private int _sex;

        public void Debug()
        {
        }
    }

}
