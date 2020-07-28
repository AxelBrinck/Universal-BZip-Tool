using System;
using System.IO;
using DecoderLib;

namespace Unbzip
{
    class Program
    {

        private static void DisplayHelp() {
            Console.WriteLine($"Universal BZip Tool {typeof(Program).Assembly.GetName().Version}, 27 Jul 2020, Axel Brinck.");
            Console.WriteLine("Can inflate all bzip stream types.");
            Console.WriteLine("Tool oriented to batch process files from a given directory.");
            Console.WriteLine("");
            Console.WriteLine("USAGE:");
            Console.WriteLine("Commands: -inflate / -deflate / -help");
            Console.WriteLine("Argument 1: Source directory. (All files must contain a bzip stream)");
            Console.WriteLine("Argument 2: Target directory.");
            Console.WriteLine("Argument 3: Extension. (Specify the extension the file h)");
            Console.WriteLine("");
            Console.WriteLine("EXAMPLES:");
            Console.WriteLine("unbzip.exe -inflate compressed_files/ raw_files/ .deflated");
            Console.WriteLine("unbzip.exe -deflate raw_files/ compressed_files/ .deflated");
            Console.WriteLine("unbzip.exe -help");
            Console.WriteLine("");
            Console.WriteLine("EXTENSION BEHAVIOUR NOTES:");
            Console.WriteLine("Deflating will append the specified extension to the original file names");
            Console.WriteLine("Inflating will remove the specified extension to the deflated file names");
        }

        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "-help" || args.Length != 4) {
                DisplayHelp();
                return;
            }

            string command = args[0];
            string inputPath = args[1];
            string outputPath = args[2];
            string extension = args[3];

            if (command != "-inflate" && command != "-deflate")
            {
                Console.WriteLine($"ERROR: Command {command} is not recognized. Use -help for more information");
                return;
            }

            if (!Directory.Exists(inputPath))
            {
                Console.WriteLine($"ERROR: Input directory {inputPath} does not exists. Use -help for more information");
                return;
            }
            
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            foreach(string inputFilePath in Directory.GetFiles(inputPath))
            {
                var fileInfo = new FileInfo(inputFilePath);

                Console.Write($"Processing: {fileInfo.Name} - {fileInfo.Length} bytes... ");

                string outputFileName = fileInfo.Name;
                
                if (command == "-deflate") outputFileName += extension;
                if (command == "-inflate") outputFileName = outputFileName.Substring(0, outputFileName.Length - extension.Length);

                if (File.Exists($"{outputPath}/{outputFileName}"))
                {
                    Console.WriteLine("SKIPPED");
                    continue;
                }

                using(var input = File.OpenRead(inputFilePath))
                using(var output = File.Create($"{outputPath}/{outputFileName}"))
                {
                    var bzip = new BZip(input, output);
                    bzip.InflateFromPrefixedBZip();
                }
                
                Console.WriteLine("OK");
            }
        }
    }
}
