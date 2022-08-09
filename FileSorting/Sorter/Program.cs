using Sorter.Services;
using System.Diagnostics;

namespace Sorter
{
    internal static class Sorter
    {
        static void Main(string[] args)
        {
            Stopwatch swMain = new Stopwatch();
            swMain.Start();

            Stopwatch sw = new Stopwatch();
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please provide input as a parameter");
                return;
            }

            string inputPath = args![0];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            // Splits file into multiple smaller files
            sw.Start();
            var splitFilePaths = new Splitter(inputPath).Split();
            sw.Stop();
            Console.WriteLine("Elapsed seconds - split = {0}", sw.Elapsed.TotalSeconds);

            // Sort each files 
            sw = new Stopwatch();
            sw.Start();
            var sortedFilePath = new FileSorter(splitFilePaths).Sort();
            sw.Stop();
            Console.WriteLine("Elapsed seconds - sort = {0}", sw.Elapsed.TotalSeconds);

            var outputFilePath = $"{Path.GetDirectoryName(inputPath)}\\{Path.GetFileNameWithoutExtension(inputPath)}-sorted{Path.GetExtension(inputPath)}";

            // Merge files into one sorted file
            sw = new Stopwatch();
            sw.Start();
            new FileMerger(sortedFilePath, outputFilePath).MergeFiles();
            sw.Stop();
            Console.WriteLine("Elapsed seconds - merge = {0}", sw.Elapsed.TotalSeconds);

            swMain.Stop();
            Console.WriteLine("Elapsed seconds - total = {0}", swMain.Elapsed.TotalSeconds);

            Console.WriteLine($"File sorted: {outputFilePath}");
        }
    }
}