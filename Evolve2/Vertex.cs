using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class Vertex : ICloneable
    {
        private Guid _ident;
        public Guid Identity
        {
            get
            {
                return _ident;
            }
            internal set
            {
                _ident = value;
            }
        }

        public State State { get; private set; }

        public Vertex():this(State.HEALTHY)
        {}

        public Vertex(State state)
        {
            this._ident = Guid.NewGuid();
            this.State = state;
        }

        public void SwitchState(State NewState)
        {
            this.State = NewState;
        }

        public object Clone()
        {
            Vertex v = new Vertex(this.State);
            v.Identity = this.Identity;

            return v;
        }
    }
}
