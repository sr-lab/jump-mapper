using System;
using System.Linq;

namespace JumpMapper.AscendingPinGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Print usage if necessary.
            if (args.Length > 3 || args.Contains("-?"))
            {
                Console.WriteLine("Usage: AscendingPinGenerator [-?] [length=4] [count=100] [start=0]");
                return;
            }

            // Parse given length.
            var length = 4;
            if (args.Length > 0)
            {
                if (!int.TryParse(args[0], out length))
                {
                    Console.WriteLine($"Length given could not be parsed, default value {length} used.");
                }
            }

            // Parse given count.
            var count = 100;
            if (args.Length > 1)
            {
                if (!int.TryParse(args[1], out count))
                {
                    Console.WriteLine($"Count given could not be parsed, default value {count} used.");
                }
            }

            // Parse given start.
            var start = 0;
            if (args.Length > 2)
            {
                if (!int.TryParse(args[2], out start))
                {
                    Console.WriteLine($"Start given could not be parsed, default value {start} used.");
                }
            }

            // Loop from start specified number of times.
            for (var i = start; i < start + count; i++)
            {
                // Check we haven't exceeded length.
                var str = i.ToString();
                if (str.Length > length)
                {
                    Console.WriteLine($"Error, the value {str} is longer than the given length {length}. Aborting.");
                    return;
                }

                // Print generated PIN.
                Console.WriteLine(str.PadLeft(length, '0'));
            }
        }
    }
}
