using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using JumpMapper.Shared;

namespace JumpMapper.PinHarvester
{
    class Program
    {
        /// <summary>
        /// Returns true if the given character is an ASCII Arabic numeral, otherwise returns false.
        /// </summary>
        /// <param name="chr">The character to check.</param>
        /// <returns></returns>
        private static bool IsArabicNumeral(char chr)
        {
            return chr >= 48 && chr <= 57;
        }

        /// <summary>
        /// Extracts every 'chunk' or 'island' of digits from a string.
        /// </summary>
        /// <param name="str">The string to extract from.</param>
        /// <returns></returns>
        private static string[] ExtractDigitChunks(string str)
        {
            var chunks = new List<string>();
            var buffer = new StringBuilder();

            // Loop *past* the end of the string.
            for (int i = 0; i <= str.Length; i++)
            {
                // If we've gone past the end of the string or read past an island.
                if (i == str.Length || !IsArabicNumeral(str[i]))
                {
                    // Anything on our buffer goes to output, then empty buffer.
                    if (buffer.Length > 0)
                    {
                        chunks.Add(buffer.ToString());
                        buffer.Clear();
                    }
                }
                else
                {
                    // Add any digit to buffer.
                    buffer.Append(str[i]);
                }
            }
            return chunks.ToArray();
        }

        /// <summary>
        /// Expands a string into an array of all its substrings of a specified length.
        /// </summary>
        /// <param name="str">The string to expand.</param>
        /// <param name="len">The length of substrings to be returned.</param>
        /// <returns></returns>
        private static string[] ExpandChunk(string str, int len)
        {
            var expanded = new List<string>();
            for (int i = 0; i <= str.Length - len; i++)
            {
                expanded.Add(str.Substring(i, len));
            }
            return expanded.ToArray();
        }

        static void Main(string[] args)
        {
            // Check file exists.
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Could not read input file.");
                return;
            }

            // Parse given length.
            var length = 4;
            if (args.Length > 0)
            {
                if (!int.TryParse(args[1], out length))
                {
                    Console.WriteLine($"Length given could not be parsed, default value {length} used.");
                }
            }

            // Populate output dictionary.
            var output = new Dictionary<string, int>();
            var maxVal = int.Parse("".PadLeft(length, '9'));
            for (int i = 0; i <= maxVal; i++)
            {
                output.Add(i.ToString().PadLeft(length, '0'), 0);
            }

            // Read input file.
            var lines = FileUtils.ReadFileAsLines(args[0]);

            // Loop over input file.
            for (var i = 0; i < lines.Length; i++)
            {
                // Process each chunk.
                var chunks = ExtractDigitChunks(lines[i]);
                for (var j = 0; j < chunks.Length; j++)
                {
                    // Expand each chunk into PINs of desired length.
                    var expanded = ExpandChunk(chunks[j], length);
                    for (var k = 0; k < expanded.Length; k++)
                    {
                        // Increment count of that chunk.
                        output[expanded[k]]++;
                    }
                }
            }

            // Now we output everything.
            foreach (var entry in output)
            {
                Console.WriteLine($"{entry.Value} {entry.Key}");
            }
        }
    }
}
