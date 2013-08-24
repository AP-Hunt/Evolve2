using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Evolve2;
using Evolve2.GraphTypeHelpers;
using Evolve2.Simulations.ModifiedMoranProcess;
using Evolve2.Util;
using Evolve2.Util.Factories;

namespace Evolve2.Examples
{
    public static class GraphTypeHelpers
    {
        public static void BaiscChain()
        {
            //The non-generic chain is an implementation of Chain<Guid>
            //which uses DefaultVertexFactory, DefaultEdgeFactory and DefaultIdentityProvider
            Chain chainHelper = new Chain();

            //The identity type must be specified because type inference can't help here
            ChainInfo<Guid> chain = chainHelper.Create(10, false);

            //Can get the vertex at the start of the chain from the chain info
            ChainPart<Guid> start = chain.StartOfChain;

            //Can navigate through the chain using NextVertex property
            //The vertex itself is the Vertex property
            Vertex<Guid> nextVertex = start.NextVertex.Vertex;

            //Can get the graph representing the chain from the Graph property
            Graph<Guid> chainGraph = chain.Graph;
        }

        public static void AdvancedChain()
        {
            //The generic chain takes a vertex factory, edge factory and identity provider.
            //Pass the default implementation of any parameters you want default behaviour for
            //The factory and identity provider implementations must match the identity type of the chain (eg Guid)
            Chain<Guid> chainHelper = new Chain<Guid>(new StatefulVertexFactory<VertexState>(new EnumState(VertexState.HEALTHY)),
                                                      new DefaultEdgeFactory(),
                                                      new DefaultIdentityProvider());

            //The chain helper can then be used as normal
            ChainInfo<Guid> chain = chainHelper.Create(10, false);
        }

        public static void BasicClique()
        {
            //The non-generic clique is an implementation of Clique<Guid>
            //which uses DefaultVertexFactory, DefaultEdgeFactory and DefaultIdentityProvider
            Clique cliqueHelper = new Clique();

            //The identity type must be specified because type inference can't help here
            CliqueInfo<Guid> clique = cliqueHelper.Create(100);

            //The graph representing the clique is the Graph property
            Graph<Guid> cliqueGraph = clique.Graph;
        }

        public static void AdvancedClique()
        {
            //The generic clique takes a vertex factory, edge factory and identity provider.
            //Pass the default implementation of any parameters you want default behaviour for
            //The factory and identity provider implementations must match the identity type of the chain (eg Guid)
            Clique<Guid> cliqueHelper = new Clique<Guid>(new StatefulVertexFactory<VertexState>(new EnumState(VertexState.HEALTHY)),
                                                         new DefaultEdgeFactory(),
                                                         new DefaultIdentityProvider());

            //The identity type must be specified because type inference can't help here
            CliqueInfo<Guid> clique = cliqueHelper.Create(100);

            //The graph representing the clique is the Graph property
            Graph<Guid> cliqueGraph = clique.Graph;
        }

        public static void BaiscBurst()
        {
            //The non-generic burst is an implementation of Burst<Guid>
            //which uses DefaultVertexFactory, DefaultEdgeFactory and DefaultIdentityProvider
            Burst burstHelper = new Burst();

            //The identity type must be specified because type inference can't help here
            BurstInfo<Guid> burst = burstHelper.Create(10, false);

            //Can get the vertex at the centre of the burst from the CentreVertex property
            Vertex<Guid> centreVertex = burst.CentreVertex;

            //Can get the graph representing the chain from the Graph property
            Graph<Guid> burstGraph = burst.Graph;
        }

        public static void AdvancedBurst()
        {
            //The generic burst takes a vertex factory, edge factory and identity provider.
            //Pass the default implementation of any parameters you want default behaviour for
            //The factory and identity provider implementations must match the identity type of the chain (eg Guid)
            Burst<Guid> burstHelper = new Burst<Guid>(new StatefulVertexFactory<VertexState>(new EnumState(VertexState.HEALTHY)),
                                                      new DefaultEdgeFactory(),
                                                      new DefaultIdentityProvider());

            //The identity type must be specified because type inference can't help here
            BurstInfo<Guid> burst = burstHelper.Create(10, false);

            //The graph representing the clique is the Graph property
            Graph<Guid> burstGraph = burst.Graph;
        }
    }
}
