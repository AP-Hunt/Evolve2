using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class SimulationRunner
    {
        private IStateSelector _stateSelector;
        private IVertexSelector _vertexSelector;
        private IVictimSelector _victimSelector;
        private RandomProvider _random;

        public SimulationRunner(IStateSelector StateSelector, IVertexSelector VertexSelector, IVictimSelector VictimSelector)
        {
            _stateSelector = StateSelector;
            _vertexSelector = VertexSelector;
            _victimSelector = VictimSelector;
            _random = new RandomProvider();
        }

        public SimulationResult RunOn(Graph G, int Repetitions, int Iterations)
        {
            SimulationResult result = new SimulationResult();
            result.RepetitionsPerformed = Repetitions;

            for (int rep = 0; rep <= Repetitions; rep++)
            {
                Graph repGraph = (Graph)G.Clone();
                int iter = 0;
                while(iter < Iterations && !graphFixated(repGraph) && !graphExtinct(repGraph))
                {
                    IEnumerable<Guid> targetState = _stateSelector.Select(repGraph, _random);
                    Guid vertex = _vertexSelector.Select(targetState, repGraph, _random);
                    IEnumerable<Guid> destinationVertices = repGraph.VerticesConnectedToVertex(vertex);
                    Guid victim = _victimSelector.Select(destinationVertices, repGraph);

                    Vertex vert = repGraph.FindVertex(vertex);
                    Vertex vict = repGraph.FindVertex(victim);

                    vict.SwitchState(vert.State);

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
            }

            return result;
        }

        private bool graphFixated(Graph G)
        {
            return G.Vertices.All(v => v.State == State.MUTANT);
        }

        private bool graphExtinct(Graph G)
        {
            return G.Vertices.All(v => v.State == State.HEALTHY);
        }
    }
}
