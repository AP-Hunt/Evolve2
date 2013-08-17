using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class Vertex<T> : GraphElement<T>, ICloneable
        where T : struct
    {
        public State State { get; private set; }

        public Vertex(Util.IIdentityProvider<T> IdentityProvider)
            : this(IdentityProvider, State.HEALTHY){}

        public Vertex(Util.IIdentityProvider<T> IdentityProvider, State state) : base (IdentityProvider)
        {
            this.State = state;
        }

        public void SwitchState(State NewState)
        {
            this.State = NewState;
        }

        public object Clone()
        {
            Vertex<T> v = new Vertex<T>(_identProvider, this.State);
            v.Identity = this.Identity;

            return v;
        }
    }
}
