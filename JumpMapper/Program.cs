﻿using System;
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
        private static string[] ReadFileAsLines(string filename)
        {
            return File.ReadAllText(filename)
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        static void Main(string[] args)
        {
            // Check number of arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: JumpMapper <training_file> <input_file> [format]");
                return;
            }

            // Check file exists.
            if (!File.Exists(args[0]) || !File.Exists(args[1]))
            {
                Console.WriteLine("Could not read one or more input file.");
                return;
            }

            // Validate format, default to plain.
            var format = "plain";
            var permittedFormats = new string[] { "plain", "coq" };
            if (args.Length > 2 && permittedFormats.Contains(args[2]))
            {
                format = args[2];
            }

            // Train model.
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

            // Process file, output results.
            var input = ReadFileAsLines(args[1]);
            Console.WriteLine("pin, vulnerability");
            for (var i = 0; i < input.Length; i++)
            {
                var pin = input[i];
                var val = pad.Lookup(pin);
                Console.WriteLine($"{pin}, {val}");
            }
        }
    }
}
