using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new PinPadModel();
            t.Process("1111", 5);
        }
    }
}
