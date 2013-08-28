using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.TimedHelpers
{
    public static class TimeRangePresence
    {
        public static Func<Graph<TIdentity>, int, IEnumerable<TIdentity>, bool> PresentBetween<TIdentity>(int Min, int Max)
            where TIdentity : struct
        {
            return (g, t, v) => (t >= Min && t <= Max)? true : false;
        }
    }
}
