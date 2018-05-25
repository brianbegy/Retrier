using System;
using NUnit.Framework;

namespace Retrier.Tests
{
    [TestFixture]
    public class OfT
    {
        private int _tries = 0;

        private int AlwaysFail()
        {
            _tries++;
            throw new Exception("This is expected.");
        }

        [Test]
        public void Test()
        {
            var ex = Assert.Throws<System.Exception>(() => Retrier.Simple.RetryNTimes(() =>
            {
                var a = AlwaysFail();
            }, 4));
            Assert.That(_tries == 5);
        }
    }
}
