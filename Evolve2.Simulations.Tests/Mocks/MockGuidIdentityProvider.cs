using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Tests.Mocks
{
    public class MockGuidIdentityProvider : Evolve2.Util.IIdentityProvider<Guid>
    {
        private string _guidString;
        public MockGuidIdentityProvider(string guidString)
        {
            _guidString = guidString;
        }

        public Guid NewIdentity()
        {
            return new Guid(_guidString);
        }

        public bool Equals(Guid a, Guid b)
        {
            return a == b;
        }
    }
}
