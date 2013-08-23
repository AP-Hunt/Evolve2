using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.GraphTypeHelpers
{
    public class ChainInfo<TIdentity> : GraphConstructInfo<TIdentity>
        where TIdentity : struct
    {
        public ChainPart<TIdentity> StartOfChain { get; private set;  }

        public ChainInfo(Graph<TIdentity> ConstructedGraph, ChainPart<TIdentity> StartOfChain)
            : base(ConstructedGraph)
        {
            this.StartOfChain = StartOfChain;
        }
    }

    public class ChainPart<TIdentity>
        where TIdentity : struct
    {
        public Vertex<TIdentity> Vertex { get; set; }
        public ChainPart<TIdentity> NextVertex { get; set; }

        public ChainPart(Vertex<TIdentity> CurrentVertex)
        {
            Vertex = CurrentVertex;
            NextVertex = null;
        }
    }
}
