using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorter.Services
{
    public class FileSorter
    {
        public List<string> SplitFilePaths { get; set; }
        public FileSorter(List<string> splitFilePaths)
        {
            SplitFilePaths = splitFilePaths;
        }

        public List<string> Sort()
        {
            List<string> sortedFilePaths = new List<string>();
            Parallel.ForEach(SplitFilePaths, filePath =>
            {
                string[] lines = File.ReadAllLines(filePath);
                Array.Sort(lines, new TextComparer());

                var sortedFilePath = $"{filePath}-sorted";
                File.WriteAllLines(sortedFilePath, lines);
                sortedFilePaths.Add(sortedFilePath);

                File.Delete(filePath);
            });

            return sortedFilePaths;
        }
    }
}
