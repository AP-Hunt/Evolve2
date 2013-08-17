using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class RandomProvider
    {
        public static Random Random;

        static RandomProvider()
        {
            Random = new Random();
        }
    }
}
