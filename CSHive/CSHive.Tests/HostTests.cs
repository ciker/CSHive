using System;
using System.Reflection;
using CS;
using CS.Diagnostics;
using NUnit.Framework;

namespace CSHive.Tests
{
    [TestFixture]
    public class HostTests
    {
        [Test]
        public void DomainTest()
        {
            Tracer.DebugModel(typeof(Environment),null);
            
        }

        [Test]
        public void RefectionTest()
        {
            var type = typeof(Environment);
            var prs = type.GetProperties();
            foreach (var pr in prs)
            {
                Tracer.Debug($"{pr.Name}:{pr.GetValue(null, null)}");
            }
        }

    }
}