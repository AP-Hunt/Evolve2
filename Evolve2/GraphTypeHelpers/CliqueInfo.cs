using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.GraphTypeHelpers
{
    // Not a particularly useful class, but its here to meet convention with the other helpers
    public class CliqueInfo<TIdentity> : GraphConstructInfo<TIdentity>
        where TIdentity : struct
    {
        public CliqueInfo(Graph<TIdentity> ConstructedGraph)
            : base(ConstructedGraph)
        { }
    }
}
