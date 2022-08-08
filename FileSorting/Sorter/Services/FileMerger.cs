using Sorter.Common;
using Sorter.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorter.Services
{
    public class FileMerger
    {
        public List<string> SortedFilePaths { get; set; }
        public string OutputFilePath { get; set; }
        private const int _numberOfLinesToRead = 100;
        private readonly int _numberOfSplitFiles;
        private readonly StreamReader?[] streams;
        private readonly Queue<QueueItem>[] queues;
        public FileMerger(List<string> sortedFilePaths, string outputFilePath)
        {
            SortedFilePaths = sortedFilePaths;
            OutputFilePath = outputFilePath;

            _numberOfSplitFiles = SortedFilePaths.Count;

            streams = new StreamReader[_numberOfSplitFiles];
            queues = new Queue<QueueItem>[_numberOfSplitFiles];
        }

        public void MergeFiles()
        {
            using (var sw = new StreamWriter(OutputFilePath))
            {
                InitiliazeStreamAndQueues();

                while (true)
                {
                    List<QueueItem> list = GetMinItemsFromQueues();

                    if (list.Count <= 0)
                    {
                        break;
                    }

                    var minElement = FindMinElementInQueues(list);
                    sw.WriteLine(minElement);
                }
            }
        }

        private string? FindMinElementInQueues(List<QueueItem> list)
        {
            list.Sort(new QueueItemComparer());
            var minElement = list[0];
            queues[minElement.FileIndex].Dequeue();

            return minElement.Text;
        }

        private List<QueueItem> GetMinItemsFromQueues()
        {
            List<QueueItem> list = new List<QueueItem>();
            for (int i = 0; i < _numberOfSplitFiles; i++)
            {
                if (queues[i].Count > 0)
                    list.Add(queues[i].Peek());
                else
                {
                    if (streams[i] == null)
                        continue;
                    else if (FeedQueue(queues[i], streams[i]!, i) > 0)
                        list.Add(queues[i].Peek());
                    else
                    {
                        streams[i]!.Close();
                        streams[i] = null;
                        File.Delete(SortedFilePaths[i]);
                    }
                }
            }

            return list;
        }

        private void InitiliazeStreamAndQueues()
        {
            for (int i = 0; i < _numberOfSplitFiles; i++)
            {
                streams[i] = new StreamReader(SortedFilePaths[i]);
                queues[i] = new Queue<QueueItem>();

                FeedQueue(queues[i], streams[i]!, i);
            }
        }

        private int FeedQueue(Queue<QueueItem> queue, StreamReader stream, int fileIndex)
        { 
            for (int i = 0; i < _numberOfLinesToRead; i++)
            {
                if (stream.Peek() >= 0)
                {
                    var line = stream.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        queue.Enqueue(new QueueItem { FileIndex = fileIndex, Text = line });
                    }
                }
                else 
                { 
                    return -1;
                }
                
            }

            return 1;
        }
    }
}
