using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.State;

namespace Evolve2.Simulations.ModifiedMoranProcess
{
    public class EnumState : StateBase<VertexState>
    {
        public EnumState(VertexState InitialValue)
            : base(InitialValue)
        { }

        public override int CompareTo(IState<VertexState> other)
        {
            return (other.CurrentState == this.CurrentState) ? 1 : -1;
        }

        public override int Compare(IState<VertexState> x, IState<VertexState> y)
        {
            return x.CompareTo(y);
        }

        public override bool ChangeStateValue(VertexState NewStateValue)
        {
            if (this.CurrentState == NewStateValue)
            {
                return false;
            }

            this.CurrentState = NewStateValue;
            return true;
        }

        public override bool StateChangeIsValid(VertexState NewStateValue)
        {
            return true;
        }

        public override object Clone()
        {
            EnumState cloned = new EnumState(this.CurrentState);
            return cloned;
        }
    }
}
