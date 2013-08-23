using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.GraphTypeHelpers;

namespace Evolve2.GraphTypeHelpers
{
    public class GraphConstructInfo<T>
        where T : struct
    {
        internal protected Graph<T> _graph;
        public Graph<T> Graph
        {
            get
            {
                return _graph;
            }
        }

        protected GraphConstructInfo(Graph<T> ConstructedGraph)
        {
            _graph = ConstructedGraph;
        }
    }
}
