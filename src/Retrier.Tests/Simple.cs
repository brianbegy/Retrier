using System;
using NUnit.Framework;

namespace Retrier.Tests
{
    [TestFixture]
    public class Simple
    {
        private int _tries = 0;

        private void AlwaysFail()
        {
            _tries++;
            throw new Exception("This is expected.");
        }

        [Test]
        public void TestMethod()
        {
            var ex = Assert.Throws<System.Exception>(() => Retrier.Simple.RetryNTimes(()=> AlwaysFail(),4));
            Assert.That(_tries==5);
        }
    }
}
