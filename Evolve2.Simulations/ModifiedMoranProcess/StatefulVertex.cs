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

        public StatefulVertex(TState StartingState, Util.IIdentityProvider<T> IdentityProvider)
            : base(IdentityProvider)
        { 
        }

        public override object Clone()
        {
            StatefulVertex<T, TState> cloned = (StatefulVertex<T, TState>)base.Clone();
            cloned.State = _state;

            return cloned;
        }
    }
}
