using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Util
{
    public interface IIdentityProvider<TIdent> 
        where TIdent : struct 
    {
        TIdent NewIdentity();
        bool Equals(TIdent a, TIdent b);
    }
}
