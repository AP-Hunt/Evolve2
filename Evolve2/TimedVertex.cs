using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.Util;

namespace Evolve2
{
    public class TimedVertex<TIdentity> : TimedGraphElement<TIdentity>
        where TIdentity : struct
    {
        public TimedVertex(IIdentityProvider<TIdentity> IdentityProvider,
                           Func<Graph<TIdentity>, int, IEnumerable<TIdentity>, bool> Present)
            : base(IdentityProvider, Present)
        { }
    }
}
