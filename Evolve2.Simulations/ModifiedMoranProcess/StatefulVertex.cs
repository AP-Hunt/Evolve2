using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class StatefulVertex<T, TState> : Vertex<T>, State.IStateful<TState>
        where TState : struct
        where T : struct
    {
        private State.IState<TState> _state;
        public State.IState<TState> State
        {
            get 
            { 
                return _state; 
            }
            private set
            {
                _state = value;
            }
        }

        public StatefulVertex(Evolve2.State.IState<TState> StartingState, Util.IIdentityProvider<T> IdentityProvider)
            : base(IdentityProvider)
        {
            _state = StartingState;
        }

        public override object Clone()
        {
            StatefulVertex<T, TState> cloned = new StatefulVertex<T, TState>((Evolve2.State.IState<TState>)this.State.Clone(), _identProvider);
            cloned.Identity = this.Identity;
            return cloned;
        }
    }
}
