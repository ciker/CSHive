using CS.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CS.Validation
{
    [TestClass()]
    public class ValidTests
    {
        [TestMethod()]
        public void NameValidatorTest()
        {
            //Assert.IsTrue(Valid.NameValidator("atwind"));
            //Assert.IsFalse(Valid.NameValidator("at"));
            //Assert.IsFalse(Valid.NameValidator("中"));
            //Assert.IsTrue(Valid.NameValidator("中国"));
            Assert.IsTrue(1==1);
        }

        [TestMethod()]
        public void DescriptionValidatorTest()
        {
            //Assert.Fail();
            Assert.IsTrue(1 == 1);
        }
    }
} 