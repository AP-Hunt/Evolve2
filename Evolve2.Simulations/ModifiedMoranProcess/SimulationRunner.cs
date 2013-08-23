using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulations.ModifiedMoranProcess
{
    public class SimulationRunner
    {
        private IStateSelector _stateSelector;
        private IVertexSelector _vertexSelector;
        private IVictimSelector _victimSelector;
        private Random _random;

        public SimulationRunner(IStateSelector StateSelector, IVertexSelector VertexSelector, IVictimSelector VictimSelector) :
            this(StateSelector, VertexSelector, VictimSelector, Util.RandomProvider.Random)
        { }
        public SimulationRunner(IStateSelector StateSelector, IVertexSelector VertexSelector, IVictimSelector VictimSelector, Random Random)
        {
            _stateSelector = StateSelector;
            _vertexSelector = VertexSelector;
            _victimSelector = VictimSelector;
            _random = Random;
        }

        public SimulationResult RunOn<TIdent>(Graph<TIdent> G, int Repetitions, int Iterations, double MutantWeight)
            where TIdent : struct
        {
            SimulationResult result = new SimulationResult();
            result.RepetitionsPerformed = Repetitions;

            for (int rep = 0; rep <= Repetitions; rep++)
            {
                Graph<TIdent> repGraph = (Graph<TIdent>)G.Clone();
                
                int iter = 1;
                while(iter < Iterations && !graphFixated(repGraph) && !graphExtinct(repGraph))
                {
                    IEnumerable<TIdent> targetState = _stateSelector.Select(repGraph, _random, MutantWeight);
                    TIdent vertex = _vertexSelector.Select(targetState, repGraph, _random);
                    IEnumerable<TIdent> destinationVertices = repGraph.VerticesConnectedToVertex(vertex); 
                    TIdent victim = _victimSelector.Select(destinationVertices, repGraph, _random);

                    StatefulVertex<TIdent, VertexState> vert = (StatefulVertex<TIdent, VertexState>)repGraph.FindVertex(vertex);
                    StatefulVertex<TIdent, VertexState> vict = (StatefulVertex<TIdent, VertexState>)repGraph.FindVertex(victim);

                    vict.State.ChangeStateValue(vert.State.CurrentState);

                    System.Diagnostics.Debug.WriteLine("{0} Mutants {1} Healthy",
                                                        repGraph.Vertices.OfType<StatefulVertex<TIdent, VertexState>>().Count(v => v.State.CurrentState == VertexState.MUTANT),
                                                        repGraph.Vertices.OfType<StatefulVertex<TIdent, VertexState>>().Count(v => v.State.CurrentState == VertexState.HEALTHY));

                    iter++;
                }

                if (!graphFixated(repGraph) && !graphExtinct(repGraph))
                {
                    result.Timeout++;
                }
                else
                {
                    if (graphFixated(repGraph) && !graphExtinct(repGraph))
                    {
                        result.Fixations++;
                    }
                    else if (!graphFixated(repGraph) && graphExtinct(repGraph))
                    {
                        result.Extinctions++;
                    }
                }

                System.Diagnostics.Debug.WriteLine("-----------------------------------------");
            }

            return result;
        }

        private bool graphFixated<T>(Graph<T> G) where T : struct
        {
            return G.Vertices.OfType<StatefulVertex<Guid, VertexState>>().All(v => v.State.CurrentState == VertexState.MUTANT);
        }

        private bool graphExtinct<T>(Graph<T> G) where T : struct
        {
            return G.Vertices.OfType<StatefulVertex<Guid, VertexState>>().All(v => v.State.CurrentState == VertexState.HEALTHY);
        }
    }
}
