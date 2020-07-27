using System;
using System.IO;
using DecoderLib;

namespace Program
{
    class Program
    {

        private static void DisplayHelp() {
            Console.WriteLine($"Universal BZip Tool {typeof(Program).Assembly.GetName().Version}, 27 Jul 2020, Axel Brinck.");
            Console.WriteLine("For batch processing usage.");
            Console.WriteLine("");
            Console.WriteLine("USAGE:");
            Console.WriteLine("Argument 1: Source directory. (All files must contain a bzip stream)");
            Console.WriteLine("Argument 2: Target directory.");
            Console.WriteLine("");
            Console.WriteLine("EXAMPLE:");
            Console.WriteLine("unbzip.exe compressed_files/ raw_files/");
        }

        static void Main(string[] args)
        {
            if (args.Length == 0) {
                DisplayHelp();
                return;
            }

            string inputDir = args[0];
            string outputDir = args[1];

            if (!Directory.Exists(inputDir) || !Directory.Exists(outputDir)) {
                Console.WriteLine("One or both directories does not exist");
                Console.WriteLine($"Input: {inputDir} Output: {outputDir}");
                return;
            }

            foreach(string inputFileName in Directory.GetFiles(inputDir))
            {
                string outputFileName = $"{inputFileName}.json";

                using(var input = File.OpenRead(inputFileName))
                using(var output = File.Create(outputFileName))
                {
                    var bzip = new BZip(input, output);
                    bzip.InflateFromPrefixedBZip();
                }

                Console.WriteLine($"Decompressed: {inputFileName}");
            }
        }
    }
}
