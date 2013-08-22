using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evolve2;

namespace Evolve2.Tests.Util
{
    [TestClass]
    public class GuidIdentityProvider
    {
        Evolve2.Util.GuidIdentityProvider Provider;

        [TestInitialize]
        public void Setup()
        {
            Provider = new Evolve2.Util.GuidIdentityProvider();
        }

        [TestCleanup]
        public void Teardown()
        {

        }

        [TestMethod]
        public void NewIdentity_ReturnsNewGuid()
        {
            Guid g = Provider.NewIdentity();

            Assert.IsNotNull(g);
        }

        [TestMethod]
        public void NewIdentity_ReturnsSubsequentUniqueGuids()
        {
            Guid g1 = Provider.NewIdentity();
            Guid g2 = Provider.NewIdentity();

            Assert.AreNotEqual<Guid>(g1, g2);
        }

        [TestMethod]
        public void Equals_IdenticalGuidObjectsAreEqual()
        {
            Guid g1 = new Guid("9FB1BD07-771D-4109-B495-6B6E64397E28");
            bool result = Provider.Equals(g1, g1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_IdenticalGuidValuesAreEqual()
        {
            Guid g1 = new Guid("9FB1BD07-771D-4109-B495-6B6E64397E28");
            Guid g2 = new Guid("9FB1BD07-771D-4109-B495-6B6E64397E28");
            bool result = Provider.Equals(g1, g2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_DiferingGuidsAreNotEqual()
        {
            Guid g1 = new Guid("9FB1BD07-771D-4109-B495-6B6E64397E28");
            Guid g2 = new Guid("B8C13250-80D6-4DCE-A941-633CCC5039E0");
            bool result = Provider.Equals(g1, g2);

            Assert.IsFalse(result);           
        }
    }
}
