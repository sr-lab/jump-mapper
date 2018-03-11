using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    /// <summary>
    /// Represents a jump from one key to another on a PIN pad.
    /// </summary>
    public class Jump
    {
        /// <summary>
        /// The x-offset of the jump.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// The y-offset of the jump.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Returns true if the given jump is identical to this one.
        /// </summary>
        /// <param name="jump">The jump to compare.</param>
        /// <returns></returns>
        public bool Same(Jump jump)
        {
            return X == jump.X && Y == jump.Y;
        }

        /// <summary>
        /// Initializes a new instance of a jump from one key to another on a PIN pad.
        /// </summary>
        /// <param name="x">The x-offset of the jump.</param>
        /// <param name="y">The y-offset of the jump.</param>
        public Jump(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
