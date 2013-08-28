using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.Util;

namespace Evolve2
{
    public abstract class TimedGraphElement<TIdentity> : GraphElement<TIdentity>
        where TIdentity : struct
    {
        private Func<Graph<TIdentity>, int, IEnumerable<TIdentity>, bool> _presenceFunc;

        public TimedGraphElement(IIdentityProvider<TIdentity> IdentityProvider,
                                 Func<Graph<TIdentity>, int, IEnumerable<TIdentity>, bool> Present)
            : base(IdentityProvider)
        {
            if (Present == null)
            {
                throw new ArgumentNullException("Present", "Present function cannot be null");
            }

            _presenceFunc = Present;
        }

        public bool IsPresentAt(Graph<TIdentity> Graph, int Time)
        {
            return _presenceFunc(Graph, Time, Graph.VerticesConnectedToVertex(this.Identity));
        }
    }
}
