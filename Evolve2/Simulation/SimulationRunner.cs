using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulation
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

        public SimulationResult RunOn<TIdent>(Graph<TIdent> G, int Repetitions, int Iterations)
            where TIdent : struct
        {
            SimulationResult result = new SimulationResult();
            result.RepetitionsPerformed = Repetitions;

            for (int rep = 0; rep <= Repetitions; rep++)
            {
                Graph<TIdent> repGraph = (Graph<TIdent>)G.Clone();
                Console.WriteLine("Starting with {0} health and {1} mutant", repGraph.Vertices.Count(v => v.State == State.HEALTHY), repGraph.Vertices.Count(v => v.State == State.MUTANT));
                int iter = 0;
                while(iter < Iterations && !graphFixated(repGraph) && !graphExtinct(repGraph))
                {
                    IEnumerable<TIdent> targetState = _stateSelector.Select(repGraph, _random);
                    TIdent vertex = _vertexSelector.Select(targetState, repGraph, _random);
                    IEnumerable<TIdent> destinationVertices = repGraph.VerticesConnectedToVertex(vertex); 
                    TIdent victim = _victimSelector.Select(destinationVertices, repGraph, _random);

                    Vertex<TIdent> vert = repGraph.FindVertex(vertex);
                    Vertex<TIdent> vict = repGraph.FindVertex(victim);

                    vict.SwitchState(vert.State);

                    iter++;

                    System.Diagnostics.Debug.WriteLine("Ending iteration with {0} healthy and {1} mutant", repGraph.Vertices.Count(v => v.State == State.HEALTHY), repGraph.Vertices.Count(v => v.State == State.MUTANT));
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
            return G.Vertices.All(v => v.State == State.MUTANT);
        }

        private bool graphExtinct<T>(Graph<T> G) where T : struct
        {
            return G.Vertices.All(v => v.State == State.HEALTHY);
        }
    }
}
