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

            sw.Start();
            var splitFilePaths = new Splitter(inputPath).Split();
            sw.Stop();
            Console.WriteLine("Elapsed seconds - split = {0}", sw.Elapsed.TotalSeconds);

            sw = new Stopwatch();
            sw.Start();
            var sortedFilePath = new FileSorter(splitFilePaths).Sort();
            sw.Stop();
            Console.WriteLine("Elapsed seconds - sort = {0}", sw.Elapsed.TotalSeconds);

            sw = new Stopwatch();
            sw.Start();
            new FileMerger(sortedFilePath, $"{inputPath}-sorted").MergeFiles();
            sw.Stop();
            Console.WriteLine("Elapsed seconds - merge = {0}", sw.Elapsed.TotalSeconds);

            swMain.Stop();
            Console.WriteLine("Elapsed seconds - total = {0}", swMain.Elapsed.TotalSeconds);
        }
    }
}