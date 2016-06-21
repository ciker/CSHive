using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void AverageTest()
        {
            var rnd = new Random();
            var rst = true;
            for (int i = 0; i < 10000; i++)
            {
                var x = rnd.Next(int.MinValue, int.MaxValue);
                var y = x.AverageToArray(rnd.Next(1,short.MaxValue));
               // Console.WriteLine($"{x}->3:{y.ToJssJson()}    Result:{y.Sum() == x}");
                rst &= (y.Sum() == x);
            }
            Assert.IsTrue(rst);
        }

        [TestMethod]
        public void AverageToArrayTest()
        {
            var val = -7;
            var y = val.AverageToArray(3);
            //Console.WriteLine($"{val}->3:{y.ToJssJson()}    Result:{y.Sum() == val}");
        }


    }
}