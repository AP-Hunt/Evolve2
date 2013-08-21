using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.State
{
    public class EnumState : StateBase<Evolve2.States>
    {
        public EnumState(Evolve2.States InitialValue)
            : base(InitialValue)
        { }

        public override int CompareTo(IState<States> other)
        {
            return (other.CurrentState == this.CurrentState) ? 1 : -1;
        }

        public override bool ChangeStateValue(States NewStateValue)
        {
            if (this.CurrentState == NewStateValue)
            {
                return false;
            }

            this.CurrentState = NewStateValue;
            return true;
        }

        public override bool StateChangeIsValid(States NewStateValue)
        {
            return true;
        }
    }
}
