using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evolve2;
using Evolve2.Util;
using Evolve2.Tests.Mocks;
using Evolve2.Simulations;
using Moran = Evolve2.Simulations.ModifiedMoranProcess;

namespace Evolve2.Simulations.Tests
{
    [TestClass]
    public class StatefulVertex
    {
        private MockGuidIdentityProvider guidProvider;

        [TestInitialize]
        public void Setup()
        {
            guidProvider = new MockGuidIdentityProvider("8ADDEB1A-04B5-442C-A037-59777350F0B5");
        }

        [TestCleanup]
        public void Teardown()
        {
            guidProvider = null;
        }

        [TestMethod]
        public void State_HealthyBySetting()
        {
            StatefulVertex<Guid, Moran.VertexState> v = new StatefulVertex<Guid, Moran.VertexState>(Moran.VertexState.HEALTHY, guidProvider);
            Assert.AreEqual<Moran.VertexState>(v.State.CurrentState, Moran.VertexState.HEALTHY);
        }

        [TestMethod]
        public void State_MutantBySetting()
        {
            StatefulVertex<Guid, Moran.VertexState> v = new StatefulVertex<Guid, Moran.VertexState>(Moran.VertexState.MUTANT, guidProvider);
            Assert.AreEqual<Moran.VertexState>(v.State.CurrentState, Moran.VertexState.MUTANT);
        }

        [TestMethod]
        public void SwitchState_MutantToHealthy()
        {
            StatefulVertex<Guid, Moran.VertexState> v = new StatefulVertex<Guid, Moran.VertexState>(Moran.VertexState.MUTANT, guidProvider);

            Assert.AreEqual<Moran.VertexState>(v.State.CurrentState, Moran.VertexState.MUTANT);
            v.State.ChangeStateValue(Moran.VertexState.HEALTHY);
            Assert.AreEqual<Moran.VertexState>(v.State.CurrentState, Moran.VertexState.HEALTHY);
        }

        [TestMethod]
        public void SwitchState_HealthyToMutant()
        {
            StatefulVertex<Guid, Moran.VertexState> v = new StatefulVertex<Guid, Moran.VertexState>(Moran.VertexState.HEALTHY, guidProvider);

            Assert.AreEqual<Moran.VertexState>(v.State.CurrentState, Moran.VertexState.HEALTHY);
            v.State.ChangeStateValue(Moran.VertexState.MUTANT);
            Assert.AreEqual<Moran.VertexState>(v.State.CurrentState, Moran.VertexState.MUTANT);
        }

        [TestMethod]
        public void Clone_ObjectReferencesNotEqual()
        {
            StatefulVertex<Guid, Moran.VertexState> v = new StatefulVertex<Guid, Moran.VertexState>(Moran.VertexState.HEALTHY, guidProvider);
            StatefulVertex<Guid, Moran.VertexState> v2 = (StatefulVertex<Guid, Moran.VertexState>)v.Clone();

            Assert.AreNotSame(v2, v);
            Assert.AreNotEqual(v2, v);
        }

        [TestMethod]
        public void Clone_StateClonedCorrectly()
        {
            StatefulVertex<Guid, Moran.VertexState> v = new StatefulVertex<Guid, Moran.VertexState>(Moran.VertexState.HEALTHY, guidProvider);
            StatefulVertex<Guid, Moran.VertexState> v2 = (StatefulVertex<Guid, Moran.VertexState>)v.Clone();

            Assert.AreEqual<Moran.VertexState>(v.State.CurrentState, v2.State.CurrentState);
        }

        [TestMethod]
        public void Clone_IdentityClonedCorrectly()
        {
            StatefulVertex<Guid, Moran.VertexState> v = new StatefulVertex<Guid, Moran.VertexState>(Moran.VertexState.HEALTHY, guidProvider);
            StatefulVertex<Guid, Moran.VertexState> v2 = (StatefulVertex<Guid, Moran.VertexState>)v.Clone();

            Assert.AreEqual<Guid>(v.Identity, v2.Identity);
            Assert.AreNotSame(v.Identity, v2.Identity);
        }
    }
}
