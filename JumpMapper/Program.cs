using JumpMapper.Shared;
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
            var pad = new PinPadModel(new NumBarPinPadLayout());
            var lines = FileUtils.ReadFileAsLines(args[0]);
            foreach (var line in lines)
            {
                var split = line.Split(';');
                if (split.Length == 2)
                {
                    pad.Process(split[0], int.Parse(split[1]));
                }
                else if (split.Length == 1)
                {
                    pad.Process(split[0], 1);
                }
            }

            // Read in file to process.
            var input = FileUtils.ReadFileAsLines(args[1]);

            // Plain output goes straight to console.
            if (format == "plain")
            {
                Console.WriteLine("pin, vulnerability");
                for (var i = 0; i < input.Length; i++)
                {
                    var pin = input[i];
                    var val = pad.Lookup(pin);
                    Console.WriteLine($"{pin}, {val}");
                }
            }
            else if (format == "coq")
            {
                // Coq format needs placing into template.
                var output = new StringBuilder();
                for (var i = 0; i < input.Length; i++)
                {
                    var pin = input[i];
                    var val = pad.Lookup(pin);
                    output.Append($"  (\"{pin}\", {val} # 1)");
                    if (i != input.Length - 1)
                    {
                        output.Append(";");
                        output.AppendLine();
                    }
                }

                // Output template to console with placeholders filled.
                Console.Write(Properties.Resources.coq_template
                    .Replace("%NAME", Path.GetFileNameWithoutExtension(args[1]) + "_pin_freqs")
                    .Replace("%PASSWORDS", output.ToString()));
            }
        }
    }
}
