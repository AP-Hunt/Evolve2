using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2;
using Evolve2.Util;

namespace Evolve2.Tests.Mocks
{
    public class MockIdentityProvider : IIdentityProvider<Guid>
    {
        private Guid _guid;

        public MockIdentityProvider(Guid identityToReturn)
        {
            _guid = identityToReturn;
        }

        public Guid NewIdentity()
        {
            return _guid;
        }

        public bool Equals(Guid a, Guid b)
        {
            return a == b;
        }
    }
}
