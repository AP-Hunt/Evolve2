using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Util
{
    public class GuidIdentityProvider : IIdentityProvider<Guid>
    {
        public Guid NewIdentity()
        {
            return Guid.NewGuid();
        }

        public bool Equals(Guid a, Guid b)
        {
            return a == b;
        }
    }
}
