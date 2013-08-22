using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Evolve2.Tests
{
    public class GraphElementImplementor : Evolve2.GraphElement<Guid>
    {
        public GraphElementImplementor(Evolve2.Util.IIdentityProvider<Guid> IdentityProvider)
            : base(IdentityProvider)
        { }
    }

    [TestClass]
    public class GraphElement
    {
        Mocks.MockGuidIdentityProvider provider;
        string guidString = "8ADDEB1A-04B5-442C-A037-59777350F0B5";

        [TestInitialize]
        public void Setup()
        {
            provider = new Mocks.MockGuidIdentityProvider(guidString);
        }

        [TestCleanup]
        public void CleanUp()
        {
            provider = null;
        }

        [TestMethod]
        public void Identity_IsT()
        {
            Evolve2.GraphElement<Guid> ge = new GraphElementImplementor(provider);
            Assert.IsInstanceOfType(ge.Identity, typeof(Guid));
        }

        [TestMethod]
        public void Identity_IsFromProvider()
        {
            Evolve2.GraphElement<Guid> ge = new GraphElementImplementor(provider);
            Assert.AreEqual(ge.Identity, new Guid(guidString));
        }
    }
}
