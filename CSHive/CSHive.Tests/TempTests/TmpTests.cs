using CS.Cryptography;
using NUnit.Framework;

namespace CSHive.Tests.TempTests
{
    [TestFixture]
    public class TmpTests
    {

        [Test]
        public void Md5Test()
        {
            var str = "xxasdfasdf3234134";
            var md5 = Md5.Encrypt(str);
            Assert.IsEmpty(md5);

        }
    }
}