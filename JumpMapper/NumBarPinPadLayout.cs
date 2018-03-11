using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    /// <summary>
    /// Represents the layout of the number bar across the top of a QWERTY keyboard.
    /// </summary>
    public class NumBarPinPadLayout : IPinPadLayout
    {
        public Dictionary<int, Jump> GetBaseOffsets()
        {
            return new Dictionary<int, Jump>() {
                { 1, new Jump(-4, 0) },
                { 2, new Jump(-3, 0) },
                { 3, new Jump(-2, 0) },
                { 4, new Jump(-1, 0) },
                { 5, new Jump(0, 0) },
                { 6, new Jump(1, 0) },
                { 7, new Jump(2, 0) },
                { 8, new Jump(3, 0) },
                { 9, new Jump(4, 0) },
                { 0, new Jump(5, 0) },
            };
        }
    }
}
