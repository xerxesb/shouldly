using NUnit.Framework;
using Shouldly;

namespace Tests
{
    public class ShouldMessageLineNumbers
    {
        [Test]
        public void Test()
        {
            321.ShouldBe(123);
        }
    }
}