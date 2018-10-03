using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Mine
    {
        public bool explosive;
        public bool flagged = false;
        public int number;
        public Mine(bool expl)
        {
            explosive = expl;
        }
    }
}
