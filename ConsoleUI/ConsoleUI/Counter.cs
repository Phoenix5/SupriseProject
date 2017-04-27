using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Counter
    {
        public int LoopCounter { get; set; } = 0;

        public override string ToString()
        {
            return LoopCounter.ToString();
        }
    }
}
