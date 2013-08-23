using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.GraphTypeHelpers
{
    public class BurstInfo<TIdentity> : GraphConstructInfo<TIdentity>
        where TIdentity : struct
    {
        public Vertex<TIdentity> CentreVertex { get; private set; }

        public BurstInfo(Graph<TIdentity> ConstructedGraph, Vertex<TIdentity> Centre)
            : base(ConstructedGraph)
        {
            this.CentreVertex = Centre;
        }
    }
}
