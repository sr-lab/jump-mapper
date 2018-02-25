using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    class Program
    {
        /// <summary>
        /// Reads a file as lines, returning it as an array of strings.
        /// </summary>
        /// <param name="filename">The filename of the file to read.</param>
        /// <returns></returns>
        private static IEnumerable<string> ReadFileAsLines(string filename)
        {
            return File.ReadAllText(filename)
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        static void Main(string[] args)
        {
            var pad = new PinPadModel();

            var lines = ReadFileAsLines(args[0]);
            foreach (var line in lines)
            {
                var split = line.Split(';');
                if (split.Length == 2)
                {
                    pad.Process(split[0], int.Parse(split[1]));
                }
            }
        }
    }
}
