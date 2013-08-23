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
    /// <summary>
    /// Examples of using the Modified Moran Process
    /// </summary>
    public static class ModifiedMoranProcess
    {
        public static void UsingAGraphTypeHelper()
        {
            //Create a clique helper
            Clique<Guid> cliqueHelper = new Clique<Guid>(VertexFactory: new StatefulVertexFactory<VertexState>(new EnumState(VertexState.HEALTHY)),
                                                         EdgeFactory: new DefaultEdgeFactory(),
                                                         IdentityProvider: new DefaultIdentityProvider());
            //Use it to construct a graph
            Graph<Guid> cliqueGraph = cliqueHelper.Create(100).Graph;

            //StatefulVertexFactory gives all vertices a default value of VertexState.HEALTHY.
            //For the simulation we need at least one mutant. The first vertex will do.
            StatefulVertex<Guid, VertexState> v = cliqueGraph.Vertices.OfType<StatefulVertex<Guid, VertexState>>().First();
            v.State.ChangeStateValue(VertexState.MUTANT);

            MoranProcessRunner moranProcess = new MoranProcessRunner(new StateSelector(), new VertexSelector(), new VictimSelector());
            MoranProcessResult result = moranProcess.RunOn(cliqueGraph, 10, 10000, 3.0d);

            Console.WriteLine("UsingAGraphTypeHelper :: Result");
            Console.WriteLine("\t Repetitions " + result.RepetitionsPerformed);
            Console.WriteLine("\t Fixations " + result.Fixations);
            Console.WriteLine("\t Extinctions " + result.Extinctions);
            Console.WriteLine("\t Timeouts" + result.Timeout);
            Console.WriteLine("\t p(Fixation) " + result.FixationProbability);
            Console.WriteLine("\t p(Extinction) " + result.ExtinctionProbability);
            Console.WriteLine("\t p(Timeout) " + result.TimeoutProbability);
        }
    }
}
