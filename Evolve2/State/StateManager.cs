using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.State
{
    public abstract class StateManager
    {
        private IState<object> _state;
        public IState<object> State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public virtual int Compare(IState<object> x, IState<object> y)
        {
            return x.CompareTo(y);
        }
    }
}
