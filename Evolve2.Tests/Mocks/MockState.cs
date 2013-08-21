using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Tests.Mocks
{
    public class MockState : Evolve2.State.IState<int>
    {
        private int _state;
        public int CurrentState
        {
            get { return _state; }
        }

        public bool ChangeStateValue(int NewStateValue)
        {
            _state = NewStateValue;
            return true;
        }

        public bool StateChangeIsValid(int NewStateValue)
        {
            return true;
        }

        public int CompareTo(State.IState<int> other)
        {
            return _state - other.CurrentState;
        }

        public MockState(int InitialValue)
        {
            _state = InitialValue;
        }
    }
}
