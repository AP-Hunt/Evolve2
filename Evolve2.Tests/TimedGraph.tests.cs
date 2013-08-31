using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evolve2;

namespace Evolve2.Tests
{
    [TestClass]
    public class TimedGraph
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
        public void CurrentTimeStep_Is0AfterConstruction()
        {
            TimedGraph<Guid> graph = new TimedGraph<Guid>(identProvider, 10);
            Assert.AreEqual(0, graph.CurrentTimeStep);
        }

        [TestMethod]
        public void CurrentTimeStep_IncrementedOnTick()
        {
            TimedGraph<Guid> graph = new TimedGraph<Guid>(identProvider, 10);

            graph.Tick();
            Assert.AreEqual(1, graph.CurrentTimeStep);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeStepsExceededException))]
        public void Tick_NoParam_Exception_WhenTimestepExceedsMax()
        {
            TimedGraph<Guid> graph = new TimedGraph<Guid>(identProvider, 0);
            graph.Tick();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Tick_StepParam_ExceptionWhenParamTooLarge()
        {
            TimedGraph<Guid> graph = new TimedGraph<Guid>(identProvider, 20);
            graph.Tick(21);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddTimedVertex_ExceptionIfNull()
        {
            TimedGraph<Guid> graph = new TimedGraph<Guid>(identProvider, 20);
            graph.AddTimedVertex(null);
        }

        [TestMethod]
        public void AddTimedVertex_PresentAtStep2_NotInVertexSetAtStep0()
        {
            Func<Graph<Guid>, int, IEnumerable<Guid>, bool> presentAt2 =
                (g, t, e) => t == 2;

            TimedGraph<Guid> graph = new TimedGraph<Guid>(identProvider, 5);
            TimedVertex<Guid> tv = new TimedVertex<Guid>(identProvider, presentAt2);

            graph.AddTimedVertex(tv);

            Assert.IsFalse(graph.Vertices.Contains(tv));
        }

        [TestMethod]
        public void AddTimedVertex_PresentAtStep2_InVertexSetAtStep2()
        {
            Func<Graph<Guid>, int, IEnumerable<Guid>, bool> presentAt2 =
                (g, t, e) => t == 2;

            TimedGraph<Guid> graph = new TimedGraph<Guid>(identProvider, 5);
            TimedVertex<Guid> tv = new TimedVertex<Guid>(identProvider, presentAt2);

            graph.AddTimedVertex(tv);
            graph.Tick(2);

            Assert.IsTrue(graph.Vertices.Contains(tv));
        }
    }
}
