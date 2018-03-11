using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    /// <summary>
    /// Represents a PIN pad layout as found on an ATM machine.
    /// </summary>
    public class AtmPinPadLayout : IPinPadLayout
    {
        public Dictionary<int, Jump> GetBaseOffsets()
        {
            return new Dictionary<int, Jump>() {
                { 1, new Jump(-1, -1) },
                { 2, new Jump(0, -1) },
                { 3, new Jump(1, -1) },
                { 4, new Jump(-1, 0) },
                { 5, new Jump(0, 0) },
                { 6, new Jump(1, 0) },
                { 7, new Jump(-1, 1) },
                { 8, new Jump(0, 1) },
                { 9, new Jump(1, 1) },
                { 0, new Jump(0, 2) },
            };
        }
    }
}
