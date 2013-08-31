using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Evolve2.Tests
{
    [TestClass]
    public class TimedVertex
    {
        Mocks.MockIdentityProvider identProvider;

        [TestInitialize]
        public void Setup()
        {
            identProvider = new Mocks.MockIdentityProvider(new Guid("738db098-a6bb-4abe-9f2f-823af5e2d74c"));
        }

        [TestCleanup]
        public void Teardown()
        {
            identProvider = null;
        }

        [TestMethod]
        public void Presence_UsesAnonymousFunction()
        {
            TimedVertex<Guid> tv = new TimedVertex<Guid>(identProvider,
                                                         Present: (g, t, e) => t == 5);

            TimedGraph<Guid> graph = new TimedGraph<Guid>(identProvider, 10);
            graph.AddTimedVertex(tv);

            Assert.AreEqual(0, graph.CurrentTimeStep);
            Assert.IsFalse(tv.IsPresentAt(graph, graph.CurrentTimeStep));

            graph.Tick(5);
            Assert.AreEqual(5, graph.CurrentTimeStep);
            Assert.IsTrue(tv.IsPresentAt(graph, graph.CurrentTimeStep));
        }
    }
}
