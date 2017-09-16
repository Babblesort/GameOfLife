using Engine;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class GenerationTests
    {
        [Test]
        public void HasExpectedDataStructure()
        {
            var generation = new Generation();
            Assert.IsInstanceOf<Dictionary<RowCol, bool>>(generation);
        }
    }
}
