using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Evolve2;
using Evolve2.State;
using Evolve2.Util;

namespace Evolve2.Simulations.EnvironmentalEvolutionaryGraph
{
    public class EnvironmentalVertex<TIdentity, TEnvironment, TIndividual> : Vertex<TIdentity>, IStateful<TIndividual>
        where TIdentity : struct
        where TEnvironment : struct
        where TIndividual : struct
    {
        private TEnvironment _background;
        public TEnvironment Background
        {
            get
            {
                return _background;
            }
        }

        private IState<TIndividual> _foreground;
        public IState<TIndividual> State
        {
            get { return _foreground;  }
        }

        private Func<TIndividual, TEnvironment, bool> _matcherFunc;
        public bool InMatchingEnvironment
        {
            get
            {
                return _matcherFunc(_foreground.CurrentState, _background);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="IdentityProvider"></param>
        /// <param name="Foreground">The starting state of the individual on the vertex</param>
        /// <param name="Background">The background colouring of the vertex</param>
        /// <param name="EnvironmentMatcher">A function returning true if the foreground and background are considered matching</param>
        public EnvironmentalVertex(IIdentityProvider<TIdentity> IdentityProvider, IState<TIndividual> Foreground, TEnvironment Background, Func<TIndividual, TEnvironment, bool> EnvironmentMatcher)
            : base(IdentityProvider)
        {
            _foreground = Foreground;
            _background = Background;
            _matcherFunc = EnvironmentMatcher;
        }

        public override object Clone()
        {
            EnvironmentalVertex<TIdentity, TEnvironment, TIndividual> cloned =
                new EnvironmentalVertex<TIdentity, TEnvironment, TIndividual>(this._identProvider, (IState<TIndividual>)_foreground.Clone(), _background, _matcherFunc);

            cloned.Identity = this.Identity;

            return cloned;
        }
    }
}
