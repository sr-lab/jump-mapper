using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    /// <summary>
    /// Represents a PIN pad layout.
    /// </summary>
    public interface IPinPadLayout
    {
        /// <summary>
        /// Gets the base array of key offsets to use for mapping.
        /// </summary>
        /// <returns></returns>
        Dictionary<int, Jump> GetBaseOffsets();
    }
}
