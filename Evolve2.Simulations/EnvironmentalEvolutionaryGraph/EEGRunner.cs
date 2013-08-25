using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulations.EnvironmentalEvolutionaryGraph
{
    public class EEGRunner
    {
        private IStateSelector _stateSelector;
        private IVertexSelector _vertexSelector;
        private IVictimSelector _victimSelector;
        private Random _random;

        public EEGRunner(IStateSelector StateSelector, IVertexSelector VertexSelector, IVictimSelector VictimSelector) :
            this(StateSelector, VertexSelector, VictimSelector, Util.RandomProvider.Random)
        { }
        public EEGRunner(IStateSelector StateSelector, IVertexSelector VertexSelector, IVictimSelector VictimSelector, Random Random)
        {
            _stateSelector = StateSelector;
            _vertexSelector = VertexSelector;
            _victimSelector = VictimSelector;
            _random = Random;
        }

        public EEGResult RunOn<TIdentity, TEnvironment, TIndividual>(Graph<TIdentity> G, int Repetitions, int Iterations, double MismatchedFitness, Evolve2.State.IState<TIndividual>[] FixatingStates)
            where TIdentity : struct
            where TEnvironment : struct
            where TIndividual : struct
        {
            // Do some pre-flight checks to make sure all the vertices are of the type we expect
            if(!G.Vertices.All(v => v.GetType().IsAssignableFrom(typeof(EnvironmentalVertex<TIdentity, TEnvironment, TIndividual>))))
            {
                throw new ArgumentException("G", "All vertices in the supplied graph must be of type EnvironmentalVertex or more derived");
            }

            EEGResult result = new EEGResult();
            result.RepetitionsPerformed = Repetitions;

            for (int rep = 0; rep < Repetitions; rep++)
            {
                Graph<TIdentity> repGraph = (Graph<TIdentity>)G.Clone();
                
                int iter = 0;
                while (iter < Iterations && !graphFixated<TIdentity, TEnvironment, TIndividual>(repGraph, FixatingStates))
                {
                    IEnumerable<TIdentity> targetState = _stateSelector.Select<TIdentity, TEnvironment, TIndividual>(repGraph, _random, MismatchedFitness);
                    TIdentity vertex = _vertexSelector.Select<TIdentity, TEnvironment, TIndividual>(targetState, repGraph, _random);
                    IEnumerable<TIdentity> destinationVertices = repGraph.VerticesConnectedToVertex(vertex);
                    TIdentity victim = _victimSelector.Select<TIdentity, TEnvironment, TIndividual>(destinationVertices, repGraph, _random);

                    //It's possible for there to be no victim.
                    //Consider the end of a chain where there are no neighbours
                    //No action should be taken, but it should still take up an iteration
                    if(!default(TIdentity).Equals(victim))
                    {
                        EnvironmentalVertex<TIdentity, TEnvironment, TIndividual> vert = (EnvironmentalVertex<TIdentity, TEnvironment, TIndividual>)repGraph.FindVertex(vertex);
                        EnvironmentalVertex<TIdentity, TEnvironment, TIndividual> vict = (EnvironmentalVertex<TIdentity, TEnvironment, TIndividual>)repGraph.FindVertex(victim);

                        vict.State.ChangeStateValue(vert.State.CurrentState);
                    }
                    iter++;
                }

                if (!graphFixated<TIdentity, TEnvironment, TIndividual>(repGraph, FixatingStates))
                {
                    result.Timeout++;
                }
                else
                {
                    result.Fixations++;
                }
            }

            return result;
        }

        private bool graphFixated<TIdentity, TEnvironment, TIndividual>(Graph<TIdentity> G, Evolve2.State.IState<TIndividual>[] FixatingStates) 
            where TIdentity : struct
            where TEnvironment : struct
            where TIndividual : struct
        {
            foreach (TIndividual state in FixatingStates)
            {
                if (G.Vertices.OfType<EnvironmentalVertex<TIdentity, TEnvironment, TIndividual>>()
                             .All(v => v.State.Equals(state)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
