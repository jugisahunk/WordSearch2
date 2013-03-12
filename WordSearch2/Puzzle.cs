using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordSearch2
{
    internal class Puzzle
    {
        internal void Process (string inputDataFilePath, StreamWriter outFile, string inputWordsFilePath){
            List<string> inputWords = ExtractDataRows(inputWordsFilePath);
            inputWords.Sort(new DescendingComparer());

            string inputData = ExtractData(inputDataFilePath);

            int rowCount, columnCount;

            columnCount = inputData.IndexOf("\r\n");
            inputData = inputData.Replace("\r\n", String.Empty);

            if (inputData.Length % columnCount != 0)
                throw new ArgumentException("Jagged array found.");

            rowCount = inputData.Length / columnCount;

            CharacterGrid characterGrid = new CharacterGrid(rowCount, columnCount, inputData.ToCharArray());

            List<Word> words = new List<Word>();
            inputWords.ForEach(x => words.Add(new Word(x)));

            characterGrid.FindWords(words);

            //Console.WriteLine();
            //Console.WriteLine("------------------------------");
            //foreach (FoundWord foundWord in characterGrid.FoundWords)
            //{
            //    Console.WriteLine(foundWord.ToString());
            //}
            //Console.WriteLine("------------------------------");
            //Console.WriteLine();

        }

        internal List<string> ExtractDataRows(string filePath)
        {
            List<string> inputRows = new List<string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                    inputRows.Add(reader.ReadLine());
            }

            return inputRows;
        }

        internal string ExtractData(string filePath)
        {
            string input;
            using (StreamReader reader = new StreamReader(filePath))
            {
                input = reader.ReadToEnd();
            }

            return input;
        }
    }
}
