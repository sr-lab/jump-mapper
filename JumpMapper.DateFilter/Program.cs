using JumpMapper.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper.DateFilter
{
    class Program
    {
        /// <summary>
        /// Gets whether or not a given string can be parsed to a valid month number (1-12).
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns></returns>
        private static bool IsValidMonth(string str)
        {
            if (int.TryParse(str, out int num))
            {
                return num >= 1 && num <= 12;
            }
            return false;
        }

        /// <summary>
        /// Gets whether or not a given string can be parsed to a valid day-of-month number (1-31).
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns></returns>
        private static bool IsValidDay(string str)
        {
            if (int.TryParse(str, out int num))
            {
                return num >= 1 && num <= 31;
            }
            return false;
        }

        /// <summary>
        /// Splits a 6-digit date number into day, month and year portions as if it were a date.
        /// </summary>
        /// <param name="str">The string to split.</param>
        /// <returns></returns>
        private static string[] SplitDate(string str)
        {
            // Only work for 6-digit PINs.
            if (str.Length != 6)
            {
                return null;
            }

            // Split 6-digit PIN into date format.
            return new string[] {
                str.Substring(0, 2),
                str.Substring(2, 2),
                str.Substring(4, 2)
            };
        }

        /// <summary>
        /// Gets whether or not the given 6-digit number string can be interpreted as a date.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns></returns>
        private static bool IsValidDate(string str)
        {
            // Try to split string into chunks. Failure results in null.
            var chunks = SplitDate(str);
            if (chunks == null)
            {
                return false;
            }

            // Work with ddmmyy, mmddyy, yymmdd or yyddmm formats.
            return (IsValidDay(chunks[0]) && IsValidMonth(chunks[1]))
                || (IsValidDay(chunks[1]) && IsValidMonth(chunks[0]))
                || (IsValidDay(chunks[1]) && IsValidMonth(chunks[2]))
                || (IsValidDay(chunks[2]) && IsValidMonth(chunks[1]));
        }

        static void Main(string[] args)
        {
            // Check file exists.
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Could not read input file.");
                return;
            }

            // Read in file, filter and write to output.
            var lines = FileUtils.ReadFileAsLines(args[0]);
            foreach(var pin in lines.Where(x => !IsValidDate(x)))
            {
                Console.WriteLine(pin);
            }
        }
    }
}
