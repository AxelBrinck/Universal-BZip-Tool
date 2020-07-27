using System;
using System.IO;
using System.IO.Compression;
using DecoderLib;

namespace Program
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Program started");

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
            
            Console.WriteLine("Program terminated");
        }
    }
}
