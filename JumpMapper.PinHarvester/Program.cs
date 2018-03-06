using JumpMapper.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper.PinHarvester
{
    class Program
    {
        /// <summary>
        /// Writes the specified number of backspace characters to the console window.
        /// </summary>
        /// <param name="len">The number of backspace characters to write.</param>
        private static void WriteBackspace(int len)
        {
            for (int i = 0; i < len; i++)
            {
                Console.Write('\b'); 
            }
        }

        /// <summary>
        /// Writes the current progress percentage to the console.
        /// </summary>
        /// <param name="previous">The previous progress as an arbitrary integer.</param>
        /// <param name="next">The next (current) progress as an arbitrary integer.</param>
        /// <param name="total">The total progress being worked towards.</param>
        /// <param name="delete">Whether or not to delete the previous progress before writing the new.</param>
        /// <returns></returns>
        private static int WriteProgress(int previous, int next, int total, bool delete = true)
        {
            // Delete existing percentage and sign.
            if (delete)
            {
                WriteBackspace(previous.ToString().Length + 1);
            }

            // Write percentage.
            var currentPercentage = (int)Math.Round((double)(next / total) * 100);
            Console.Write(currentPercentage + "%");

            return currentPercentage;
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
                if (i == str.Length || !char.IsDigit(str[i]))
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

            // Read input file.
            var lines = FileUtils.ReadFileAsLines(args[0]);
            var total = lines.Length;

            // Show percentage display.
            var progress = WriteProgress(0, 0, total, false);
            
            // Loop over input file.
            var output = new Dictionary<string, int>();
            for (var i = 0; i < lines.Length; i++)
            {
                // Process each chunk.
                var chunks = ExtractDigitChunks(lines[i]);
                for (var j = 0; j < chunks.Length; j++)
                {
                    // Expand each chunk into PINs of desired length.
                    var expanded = ExpandChunk(chunks[i], length);
                    for (var k = 0; k < expanded.Length; k++)
                    {
                        // Increment count of that chunk.
                        if (!output.ContainsKey(expanded[k]))
                        {
                            output.Add(expanded[k], 0);
                        }
                        output[expanded[k]]++;
                    }
                }

                // Update progress.
                progress = WriteProgress(progress, i, total);
            }

            // End progress line.
            Console.WriteLine();

            // Now we output everything.
            foreach (var entry in output)
            {
                Console.WriteLine($"{entry.Value} {entry.Key}");
            }
        }
    }
}
