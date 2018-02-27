using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper.RandomPinGenerator
{
    class Program
    {
        /// <summary>
        /// The characters allowed in PINs.
        /// </summary>
        private static char[] chars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        /// The Random instance used to generate randomness.
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// Generates a random PIN.
        /// </summary>
        /// <param name="length">The length of the PIN to generate.</param>
        /// <returns></returns>
        private static string GenerateRandomPin(int length)
        {
            string output = "";
            while (output.Length < length)
            {
                output += chars[rand.Next(chars.Length)];
            }
            return output;
        }

        static void Main(string[] args)
        {
            // Print usage if necessary.
            if (args.Length > 2 || args.Contains("-?"))
            {
                Console.WriteLine("Usage: RandomPinGenerator [-?] <length> <count>");
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

            // Generate unique random PINs.
            List<string> output = new List<string>();
            while (output.Count <= count)
            {
                string buffer = null;
                while (buffer == null || output.Contains(buffer))
                {
                    buffer = GenerateRandomPin(length);
                }
                output.Add(buffer);
            }

            // Write output to console.
            for (var i = 0; i < output.Count; i++)
            {
                Console.WriteLine(output[i]);
            }
        }
    }
}
