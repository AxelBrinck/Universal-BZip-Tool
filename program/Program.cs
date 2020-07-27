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

            foreach(string arg in args)  
            {
                using (FileStream input = File.OpenRead(arg))
                {
                    input.Seek(2, SeekOrigin.Begin);

                    string newFileName = $"{arg}.json";

                    using (FileStream output = File.Create(newFileName))
                    {
                        using (DeflateStream  bzis = new DeflateStream (input, CompressionMode.Decompress))
                        {
                            bzis.CopyTo(output);
                            Console.WriteLine("Decompressed: {0}", arg);
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
