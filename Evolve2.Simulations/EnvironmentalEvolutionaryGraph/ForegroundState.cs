using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.State;

namespace Evolve2.Simulations.EnvironmentalEvolutionaryGraph
{
    public class ForegroundState : StateBase<IndividualType>
    {
        public ForegroundState(IndividualType StartState) : base(StartState)
        { }

        public override int Compare(IState<IndividualType> x, IState<IndividualType> y)
        {
            return x.CompareTo(y);
        }

        public override int CompareTo(IState<IndividualType> other)
        {
            int currentState = (int)this.CurrentState;
            int otherState = (int)other.CurrentState;

            return (currentState == otherState) ? 0 : 1;
        }

        public override bool ChangeStateValue(IndividualType NewStateValue)
        {
            if (StateChangeIsValid(NewStateValue))
            {
                this.CurrentState = NewStateValue;
                return true;
            }

            return false;
        }

        public override bool StateChangeIsValid(IndividualType NewStateValue)
        {
            return true;
        }

        public override bool Equals(IState<IndividualType> other)
        {
            return (this.CurrentState == other.CurrentState);
        }

        public override object Clone()
        {
            return new ForegroundState(this.CurrentState);
        }


    }
}
