using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFileGenerator
{
    internal class Generator
    {
        public long NumberOfLines { get; set; }
        public string OutputPath { get; set; }

        private const string _outputPathUsedWords = "used-words.dat";
        private const string _characters = "abcdefghijklmnopqrstuvwxyz";
        private const int _maxStringLength = 32;
        private const int _repeatWordProbablityPercent = 1;

        private readonly Random _randomizer;


        public Generator(long numberOfLines, string outputPath)
        {
            NumberOfLines = numberOfLines;
            OutputPath = outputPath;
            _randomizer = new Random();
        }
        public void Generate()
        {
            File.WriteAllText(_outputPathUsedWords, string.Empty);

            using (StreamWriter sw = File.CreateText(OutputPath))
            {
                for(long i = 0; i < NumberOfLines; i++)
                {
                    var number = GenerateNumber();
                    var word = GenerateWord();

                    sw.WriteLine($"{number}. {word}");
                }
                
            }
        }

        private int GenerateNumber()
        {
            return _randomizer.Next();
        }

        private int GenerateStringLength()
        {
            return _randomizer.Next(1, _maxStringLength);
        }

        private string GenerateWord()
        {
            if (ShouldGenerateNewWord())
                return GenerateNewWord();
            else
                return RepeatWord();
        }

        private bool ShouldGenerateNewWord()
        {
            int chance = _randomizer.Next(1, 101);
            return chance > _repeatWordProbablityPercent;
        }

        private string GenerateNewWord()
        {
            var word =  new string(
                Enumerable.Repeat(_characters, GenerateStringLength())
                .Select(s => s[_randomizer.Next(s.Length)]).ToArray());

            File.AppendAllLines(_outputPathUsedWords, new List<string> { word });

            return word;
        }

        private string RepeatWord()
        {
            string[] lines = File.ReadAllLines(_outputPathUsedWords);
            if (lines == null || lines.Length == 0)
                return GenerateNewWord();

            return lines[_randomizer.Next(lines.Length)];
        }
    }
}
