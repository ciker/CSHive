using System;
using System.Xml.Schema;
using CS.Validation;
using NUnit.Framework;

namespace CSHive.Tests.Validation
{
    [TestFixture]
    public class ValidatorTests
    {
    
        [Test]
        public void NameTests()
        {
            Assert.IsFalse(Valid.NameValidator("e132"));
            Assert.IsTrue(Valid.NameValidator("中文"));
            Assert.IsTrue(Valid.NameValidator("3333355"));
        }

        [Test]
        public void DescriptionTests()
        {
            Assert.IsTrue(Valid.DescriptionValidator(null));
            Assert.IsTrue(Valid.DescriptionValidator(""));
            Assert.IsFalse(Valid.DescriptionValidator(" "));
            Assert.IsTrue(Valid.DescriptionValidator(" asdfsadfsadfsadfsa   dfsadfsdfsadf "));
            Assert.IsTrue(Valid.DescriptionValidator(" 中文 中文 中文 中文 "));

            Assert.True(1 == 1);
        }

    }
}