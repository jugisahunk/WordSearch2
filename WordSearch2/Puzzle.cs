using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    internal class Puzzle
    {
        internal void Process (string inputDataFilePath, StreamWriter outFile, string inputWordsFilePath){
            List<string> inputWords = ExtractData(inputWordsFilePath).;

            inputWords.Sort(new DescendingComparer());            
            List<string> inputData = ExtractData(inputDataFilePath);



            CharacterGrid characterGrid = new CharacterGrid(inputData.Count, inputData[0].Length, String.Join("",0, inputData.ToArray<char>()));
            

            //characterGrid.FindWords(inputWords.SelectMany(x => new Word(x)));

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
