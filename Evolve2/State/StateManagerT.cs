using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.State
{
    public class StateManager<TState> : StateManager, IComparer<IState<TState>>
    {
        private IState<TState> _state;
        public new IState<TState> State
        {
            get
            {
                return _state;
            }
        }

        public StateManager(IState<TState> InitialState)
        {
            _state = InitialState;
        }

        public int Compare(IState<TState> x, IState<TState> y)
        { 
            return x.CompareTo(y);
        }
    }
}
