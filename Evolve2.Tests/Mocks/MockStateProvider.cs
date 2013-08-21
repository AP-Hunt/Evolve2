using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Tests.Mocks
{
    public class MockStateManager : Evolve2.State.StateManager<int>
    {
        public MockStateManager(int InitialValue) : base(new MockState(InitialValue))
        {
            this.State.ChangeStateValue(InitialValue);
        }
    }
}
