using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.GraphTypeHelpers;

namespace Evolve2.GraphTypeHelpers
{
    public class GraphConstructInfo<TIdentity>
        where TIdentity : struct
    {
        internal protected Graph<TIdentity> _graph;
        public Graph<TIdentity> Graph
        {
            get
            {
                return _graph;
            }
        }

        protected GraphConstructInfo(Graph<TIdentity> ConstructedGraph)
        {
            _graph = ConstructedGraph;
        }
    }
}
