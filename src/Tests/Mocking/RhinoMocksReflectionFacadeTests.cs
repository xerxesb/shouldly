using System.Collections;
using NUnit.Framework;
using Shouldly;
using Shouldly.Mocking;

namespace Tests.Mocking
{
    [AssemblyDependentTestFixture("Rhino.Mocks")]
    public class RhinoMocksReflectionFacadeTests
    {
        [Test]
        public void CanCreateMockInstance()
        {
            var mock = RhinoMocksReflectionFacade.GenerateMock<IList>();
            mock.GetType().FullName.ShouldContain("Proxy");
        }

        public interface IPolitician
        {
            void TalkCrap();
        }

        [Test]
        public void AssertWasCalledOnInstance_ShouldPass()
        {
            var politician = RhinoMocksReflectionFacade.GenerateMock<IPolitician>();

            politician.TalkCrap();

            RhinoMocksReflectionFacade.AssertWasCalled(politician, x => x.TalkCrap());
        }
    }
}
