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
        WordSearch2.CharacterGrid _testGrid;
        int _rowCount;
        int _columnCount;
        char[] _characters;

        List<string> _rows;
        private const string  AMERICA = "america", KITCHEN = "kitchen", FOREST_CAKE = "forestcake", BAD_WOLF = "badwolf", CAKE = "cake";
        private List<string> _words;

        private CharacterGrid _characterGrid;

        [TestInitialize]
        public void Initialize()
        {
            _rows = new List<string>(){
                "----------------------------america-------------------------",
                "------------------------------------------------------------",
                "------------------------------------------------------------",
                "------------------------------------------------------------",
                "---------------------e-------------------c------------------",
                "--------------nehctik---------------f----a------------------",
                "-------------------a-----------------l---k------------------",
                "------------------c-------------------o--e------------------",
                "-----------------t---------------------w--------------------",
                "----------------s-----------------------d-------------------",
                "---------------e-------------------------a------------------",
                "--------------r---------------------------b-----------------",
                "-------------o----------------------------------------------",
                "------------f-----------------------------------------------",
                "------------------------------------------------------------"
            };

            _words = new List<string>() { AMERICA, BAD_WOLF, CAKE, FOREST_CAKE, KITCHEN};

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
        public void LookForWord_HorizontalWordExists()
        {
            FoundWord foundWord = _characterGrid.FindWord(new Word(AMERICA), 28);

            Assert.IsTrue(foundWord != null);
            Assert.IsTrue(foundWord.Coordinates.A.X == 28 && foundWord.Coordinates.A.Y == 0);
            Assert.IsTrue(foundWord.Coordinates.B.X == 34 && foundWord.Coordinates.A.Y == 0);
        }

        [TestMethod]
        public void LookForWord_VerticalWordExists()
        {
            FoundWord foundWord = _characterGrid.FindWord(new Word(CAKE), 281);

            Assert.IsTrue(foundWord != null);
            Assert.IsTrue(foundWord.Coordinates.A.X == 41 && foundWord.Coordinates.A.Y == 4);
            Assert.IsTrue(foundWord.Coordinates.B.X == 41 && foundWord.Coordinates.A.Y == 7);
        }

        [TestMethod]
        public void LookForWord_BTRLExists()
        {
            FoundWord foundWord = _characterGrid.FindWord(new Word(BAD_WOLF), 690);

            Assert.IsTrue(foundWord != null);
            Assert.IsTrue(foundWord.Coordinates.A.X == 42 && foundWord.Coordinates.A.Y == 11);
            Assert.IsTrue(foundWord.Coordinates.B.X == 36 && foundWord.Coordinates.A.Y == 5);
        }

        [TestMethod]
        public void LookForWord_RightToLeftDiagonalWordExists()
        {
            FoundWord foundWord = _characterGrid.FindWord(new Word(FOREST_CAKE),780);

            Assert.IsTrue(foundWord != null);
            Assert.IsTrue(foundWord.Coordinates.A.X == 12 && foundWord.Coordinates.A.Y == 4);
            Assert.IsTrue(foundWord.Coordinates.B.X == 20 && foundWord.Coordinates.A.Y == 12);
        }
    }
}
