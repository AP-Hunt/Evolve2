using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Util
{
    public class GuidIdentityProvider : IdentityProvider<Guid>
    {
        public override Guid NewIdentity()
        {
            return Guid.NewGuid();
        }

        public override bool Equals(Guid a, Guid b)
        {
            return a == b;
        }
    }
}
