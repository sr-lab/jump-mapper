using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    class Jump
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public bool Same(Jump c)
        {
            return X == c.X && Y == c.Y;
        }

        public Jump(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
