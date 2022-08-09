using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorter.Services
{
    public class Splitter
    {
        public string InputPath { get; set; }
        private const int _chunkSizeInMb = 50;
        private const byte _newLineByte = 0xA;
        public Splitter(string inputPath)
        {
            InputPath = inputPath;
        }

        public List<string> Split()
        {
            List<string> splitFilePaths = new List<string>();
            long splitIndex = 0;
            int bufferSize = 4000; // 4 kB 
            byte[] readBuffer = new byte[bufferSize]; 
            
            using (var inputStream = File.OpenRead(InputPath))
            {
                while(inputStream.Position < inputStream.Length)
                {
                    var splitFilePath = $"{InputPath}.part{splitIndex}";
                    splitFilePaths.Add(splitFilePath);
                    WriteToSplitFile(bufferSize, readBuffer, inputStream, splitFilePath);

                    splitIndex++;
                }
            }

            return splitFilePaths;
        }

        private static void WriteToSplitFile(int bufferSize, byte[] readBuffer, FileStream inputStream, string splitFilePath)
        {
            using (var writer = File.Create(splitFilePath))
            {
                int remainingInChunk = _chunkSizeInMb * 1000000;
                while (remainingInChunk > 0)
                {
                    var readBytes = inputStream.Read(readBuffer, 0, bufferSize > remainingInChunk ? remainingInChunk : bufferSize);
                    remainingInChunk -= readBytes;

                    if (readBytes <= 0)
                    {
                        break;
                    }
                    else
                    {
                        writer.Write(readBuffer, 0, readBytes);
                        var lastCharacter = readBuffer[readBytes - 1];

                        // Stop writing to file on a line end
                        if (lastCharacter != _newLineByte && remainingInChunk <= 0)
                        {
                            remainingInChunk = 1;
                        }
                    }
                }
            }
        }
    }
}
