using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WordSearch2;

namespace UnitTestProject1
{
    [TestClass]
    public class CharacterGridTest
    {
        //WordSearch2.CharacterGrid _testGrid;
        int _rowCount;
        int _columnCount;
        char[] _characters;

        List<string> _rows;
        private const string  
            AMERICA = "america", KITCHEN = "kitchen", FOREST_CAKE = "forestcake", BAD_WOLF = "badwolf", CAKE = "cake", 
            KICK = "kick", MISERY = "misery", ALIMONY = "alimony", STARS = "stars", STAR = "star", STARRY = "starry", STARSHIP = "starship";
        private List<string> _words;

        private CharacterGrid _characterGrid;

        [TestInitialize]
        public void Initialize()
        {
            _rows = new List<string>(){
                "---------------------------america--------------------------", // 0   0 - 59
                "------------------------------------------------------------", // 1  60 - 119
                "---------------------------p--------------------------------", // 2 120 - 179
                "---------------------------i--------------------------------", // 3 180 - 239
                "---------------------e-----h-------------c------------------", // 4 240 - 299
                "--------------nehctik------s--------f----a------------------", // 5 300 - 359
                "-------------------ac------r---------l---k------------------", // 6 360 - 419
                "------------------c-i------a----------o--e------------------", // 7 420 - 479
                "------a----------t--k------t-----------w--------------------", // 8 480 - 539
                "-----l----------s-----yrratstars--------d-------------m-----", // 9 540 - 599
                "----i----------e-----------t-------------a-------------i----", //10 600 - 659
                "---m----------r------------a--------------b-------------s---", //11 660 - 719
                "--o----------o-------------r-----------------------------e--", //12 720 - 779
                "-n----------f---------------------------------------------r-", //13 780 - 839
                "y----------------------------------------------------------y"  //14 840 = 899
            };

            _words = new List<string>() { AMERICA, BAD_WOLF, CAKE, FOREST_CAKE, KITCHEN, KICK, MISERY, ALIMONY, STARS, STAR, STARRY, STARSHIP};

            _rowCount = _rows.Count;
            _columnCount = 60;

            _characters = new char[_rowCount * _columnCount];

            int k = 0;
            for (int i = 0; i < _rowCount; i++ )
            {
                char[] row = _rows[i].ToCharArray();
                for (int j = 0; j < row.Length; j++)
                {
                    _characters[k++] = row[j];
                }
            }

            _characterGrid = new WordSearch2.CharacterGrid(_rowCount, _columnCount, _characters);
        }
        
        [TestMethod]
        public void LookForWord_LRExists()
        {
            _characterGrid.FindWord(new Word(AMERICA), 27);

            Assert.IsTrue(_characterGrid.FoundWords.Count == 1);

            FoundWord foundWord = _characterGrid.FoundWords[0];

            Assert.IsTrue(foundWord.Coordinates.A.X == 27 && foundWord.Coordinates.A.Y == 0);
            Assert.IsTrue(foundWord.Coordinates.B.X == 33 && foundWord.Coordinates.B.Y == 0);
        }

        [TestMethod]
        public void LookForWord_RLExists()
        {
            _characterGrid.FindWord(new Word(KITCHEN), 320);

            Assert.IsTrue(_characterGrid.FoundWords.Count == 1);

            FoundWord foundWord = _characterGrid.FoundWords[0];

            Assert.IsTrue(foundWord.Coordinates.A.X == 20 && foundWord.Coordinates.A.Y == 5);
            Assert.IsTrue(foundWord.Coordinates.B.X == 14 && foundWord.Coordinates.B.Y == 5);
        }

        [TestMethod]
        public void LookForWord_TBExists()
        {
            _characterGrid.FindWord(new Word(CAKE), 281);

            Assert.IsTrue(_characterGrid.FoundWords.Count == 1);

            FoundWord foundWord = _characterGrid.FoundWords[0];

            Assert.IsTrue(foundWord.Coordinates.A.X == 41 && foundWord.Coordinates.A.Y == 4);
            Assert.IsTrue(foundWord.Coordinates.B.X == 41 && foundWord.Coordinates.B.Y == 7);
        }

        [TestMethod]
        public void LookForWord_BTExists()
        {
            _characterGrid.FindWord(new Word(KICK), 500);

            Assert.IsTrue(_characterGrid.FoundWords.Count == 1);

            FoundWord foundWord = _characterGrid.FoundWords[0];

            Assert.IsTrue(foundWord.Coordinates.A.X == 20 && foundWord.Coordinates.A.Y == 8);
            Assert.IsTrue(foundWord.Coordinates.B.X == 20 && foundWord.Coordinates.B.Y == 5);
        }

        [TestMethod]
        public void LookForWord_BTRLExists()
        {
            _characterGrid.FindWord(new Word(BAD_WOLF), 702);

            Assert.IsTrue(_characterGrid.FoundWords.Count == 1);

            FoundWord foundWord = _characterGrid.FoundWords[0];

            Assert.IsTrue(foundWord.Coordinates.A.X == 42 && foundWord.Coordinates.A.Y == 11);
            Assert.IsTrue(foundWord.Coordinates.B.X == 36 && foundWord.Coordinates.B.Y == 5);
        }

        [TestMethod]
        public void LookForWord_BTLRExists()
        {
            _characterGrid.FindWord(new Word(FOREST_CAKE), 792);

            Assert.IsTrue(_characterGrid.FoundWords.Count == 1);

            FoundWord foundWord = _characterGrid.FoundWords[0];

            Assert.IsTrue(foundWord.Coordinates.A.X == 12 && foundWord.Coordinates.A.Y == 13);
            Assert.IsTrue(foundWord.Coordinates.B.X == 21 && foundWord.Coordinates.B.Y == 4);
        }

        [TestMethod]
        public void LookForWord_TBRLExists()
        {
            _characterGrid.FindWord(new Word(ALIMONY), 486);

            Assert.IsTrue(_characterGrid.FoundWords.Count == 1);

            FoundWord foundWord = _characterGrid.FoundWords[0];

            Assert.IsTrue(foundWord.Coordinates.A.X == 6 && foundWord.Coordinates.A.Y == 8);
            Assert.IsTrue(foundWord.Coordinates.B.X == 0 && foundWord.Coordinates.B.Y == 14);
        }

        [TestMethod]
        public void LookForWord_TBLRExists()
        {
            _characterGrid.FindWord(new Word(MISERY), 594);

            Assert.IsTrue(_characterGrid.FoundWords.Count == 1);

            FoundWord foundWord = _characterGrid.FoundWords[0];

            Assert.IsTrue(foundWord.Coordinates.A.X == 54 && foundWord.Coordinates.A.Y == 9);
            Assert.IsTrue(foundWord.Coordinates.B.X == 59 && foundWord.Coordinates.B.Y == 14);
        }

        [TestMethod]
        public void LookForWords_PrematureCapture()
        {
            List<Word> words = new List<Word>(){
                new Word(STAR),
                new Word(STARS),
                new Word(STARSHIP),
                new Word(STARRY)};

            _characterGrid.FindWords(words);

            Assert.IsTrue(_characterGrid.FoundWords.Count == 4);

            Assert.IsTrue(_characterGrid.FoundWords[0].Coordinates.A.X == 27
                       && _characterGrid.FoundWords[0].Coordinates.A.Y == 9);
            Assert.IsTrue(_characterGrid.FoundWords[0].Coordinates.B.X == 27
                       && _characterGrid.FoundWords[0].Coordinates.B.Y == 2);

            Assert.IsTrue(_characterGrid.FoundWords[1].Coordinates.A.X == 27
                       && _characterGrid.FoundWords[1].Coordinates.A.Y == 9);
            Assert.IsTrue(_characterGrid.FoundWords[1].Coordinates.B.X == 31
                       && _characterGrid.FoundWords[1].Coordinates.B.Y == 9);

            Assert.IsTrue(_characterGrid.FoundWords[2].Coordinates.A.X == 27
                       && _characterGrid.FoundWords[2].Coordinates.A.Y == 9);
            Assert.IsTrue(_characterGrid.FoundWords[2].Coordinates.B.X == 22
                       && _characterGrid.FoundWords[2].Coordinates.B.Y == 9);

            Assert.IsTrue(_characterGrid.FoundWords[3].Coordinates.A.X == 27
                       && _characterGrid.FoundWords[3].Coordinates.A.Y == 9);
            Assert.IsTrue(_characterGrid.FoundWords[3].Coordinates.B.X == 27
                       && _characterGrid.FoundWords[3].Coordinates.B.Y == 12);


        }
    }
}
