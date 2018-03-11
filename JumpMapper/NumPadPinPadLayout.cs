using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    /// <summary>
    /// Represents the layout of the number pad on the right of a QWERTY keyboard.
    /// </summary>
    public class NumPadPinPadLayout : IPinPadLayout
    {
        public Dictionary<int, Jump> GetBaseOffsets()
        {
            return new Dictionary<int, Jump>() {
                { 1, new Jump(-1, 1) },
                { 2, new Jump(0, 1) },
                { 3, new Jump(1, 1) },
                { 4, new Jump(-1, 0) },
                { 5, new Jump(0, 0) },
                { 6, new Jump(1, 0) },
                { 7, new Jump(-1, -1) },
                { 8, new Jump(0, -1) },
                { 9, new Jump(1, -1) },
                { 0, new Jump(0, 2) },
            };
        }
    }
}
