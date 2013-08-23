using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.GraphTypeHelpers
{
    public class BurstInfo<T> : GraphConstructInfo<T>
        where T : struct
    {
        public Vertex<T> CentreVertex { get; private set; }

        public BurstInfo(Graph<T> ConstructedGraph, Vertex<T> Centre)
            : base(ConstructedGraph)
        {
            this.CentreVertex = Centre;
        }
    }
}
