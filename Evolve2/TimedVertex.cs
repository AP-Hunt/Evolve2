using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.Util;

namespace Evolve2
{
    public class TimedVertex<TIdentity> : Vertex<TIdentity>, ITimedGraphElement<TIdentity>
        where TIdentity : struct
    {
        private Func<Graph<TIdentity>, int, IEnumerable<TIdentity>, bool> _presentFunc;

        public TimedVertex(IIdentityProvider<TIdentity> IdentityProvider,
                           Func<Graph<TIdentity>, int, IEnumerable<TIdentity>, bool> Present)
            : base(IdentityProvider)
        {
            _presentFunc = Present;
        }

        public bool IsPresentAt(Graph<TIdentity> Graph, int Time)
        {
            return _presentFunc(Graph, Time, Graph.VerticesConnectedToVertex(this.Identity));
        }
    }
}
