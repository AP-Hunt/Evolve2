using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evolve2;
using Evolve2.Util;
using Evolve2.Tests.Mocks;

namespace Evolve2.Tests
{
    [TestClass]
    public class Vertex
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
        public void State_HealthyByDefault()
        {
            Vertex<Guid> v = new Vertex<Guid>(guidProvider);
            Assert.AreEqual<State>(v.State, State.HEALTHY);
        }

        [TestMethod]
        public void State_HealthyBySetting()
        {
            Vertex<Guid> v = new Vertex<Guid>(guidProvider, State.HEALTHY);
            Assert.AreEqual<State>(v.State, State.HEALTHY);
        }

        [TestMethod]
        public void State_MutantBySetting()
        {
            Vertex<Guid> v = new Vertex<Guid>(guidProvider, State.MUTANT);
            Assert.AreEqual<State>(v.State, State.MUTANT);
        }

        [TestMethod]
        public void SwitchState_MutantToHealthy()
        {
            Vertex<Guid> v = new Vertex<Guid>(guidProvider, State.MUTANT);

            Assert.AreEqual<State>(v.State, State.MUTANT);
            v.SwitchState(State.HEALTHY);
            Assert.AreEqual<State>(v.State, State.HEALTHY);
        }

        [TestMethod]
        public void SwitchState_HealthyToMutant()
        {
            Vertex<Guid> v = new Vertex<Guid>(guidProvider, State.HEALTHY);

            Assert.AreEqual<State>(v.State, State.HEALTHY);
            v.SwitchState(State.MUTANT);
            Assert.AreEqual<State>(v.State, State.MUTANT);
        }

        [TestMethod]
        public void Clone_ObjectReferencesNotEqual()
        {
            Vertex<Guid> v = new Vertex<Guid>(guidProvider);
            Vertex<Guid> v2 = (Vertex<Guid>)v.Clone();

            Assert.AreNotSame(v2, v);
            Assert.AreNotEqual(v2, v);
        }

        [TestMethod]
        public void Clone_StatesClonedCorrectly()
        {
            Vertex<Guid> v = new Vertex<Guid>(guidProvider);
            Vertex<Guid> v2 = (Vertex<Guid>)v.Clone();

            Assert.AreEqual<State>(v.State, v2.State);
        }

        [TestMethod]
        public void Clone_IdentityClonedCorrectly()
        {
            Vertex<Guid> v = new Vertex<Guid>(guidProvider);
            Vertex<Guid> v2 = (Vertex<Guid>)v.Clone();

            Assert.AreEqual<Guid>(v.Identity, v2.Identity);
            Assert.AreNotSame(v.Identity, v2.Identity);
        }
    }
}
