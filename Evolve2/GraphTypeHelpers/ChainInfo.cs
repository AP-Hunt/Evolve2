using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.GraphTypeHelpers
{
    public class ChainInfo<T> : GraphConstructInfo<T>
        where T : struct
    {
        public ChainPart<T> StartOfChain { get; private set;  }

        public ChainInfo(Graph<T> ConstructedGraph, ChainPart<T> StartOfChain)
            : base(ConstructedGraph)
        {
            this.StartOfChain = StartOfChain;
        }
    }

    public class ChainPart<T>
        where T : struct
    {
        public Vertex<T> Vertex { get; set; }
        public ChainPart<T> NextVertex { get; set; }

        public ChainPart(Vertex<T> CurrentVertex)
        {
            Vertex = CurrentVertex;
            NextVertex = null;
        }
    }
}
