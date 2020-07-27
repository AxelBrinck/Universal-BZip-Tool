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

            string inputDir = args[0];
            string outputDir = args[1];

            if (!Directory.Exists(inputDir) || !Directory.Exists(outputDir)) {
                Console.WriteLine("Directories does not exist");
                Console.WriteLine($"Input: {inputDir} Output: {outputDir}");
                return;
            }

            foreach(string file in Directory.GetFiles(inputDir))
            {
                using (FileStream input = File.OpenRead(file))
                {
                    input.Seek(2, SeekOrigin.Begin);

                    string newFileName = $"{file}.json";

                    using (FileStream output = File.Create(newFileName))
                    {
                        using (DeflateStream bzis = new DeflateStream (input, CompressionMode.Decompress))
                        {
                            bzis.CopyTo(output);
                            Console.WriteLine("Decompressed: {0}", file);
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
