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
                "wwwwwwwwwwwwwwwwwwwwwwwwwwwwamericawwwwwwwwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwwwwwwwewwwwwwwwwwwwwwwwwwwcwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwnehctikwwwwwwwwwwwwwwwfwwwwawwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwwwwwawwwwwwwwwwwwwwwwwlwwwkwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwwwwcwwwwwwwwwwwwwwwwwwwowwewwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwwwtwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwwswwwwwwwwwwwwwwwwwwwwwwwdwwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwewwwwwwwwwwwwwwwwwwwwwwwwwawwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwbwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwowwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwfwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww",
                "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww"
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
        public void FindWord_HorizontalWordExists()
        {
            FoundWord foundWord = _characterGrid.FindWord(new Word(AMERICA));

            Assert.IsTrue(foundWord != null);
            Assert.IsTrue(foundWord.A.x == 28 && foundWord.A.y == 0);
            Assert.IsTrue(foundWord.B.x == 34 && foundWord.A.y == 0);
        }

        [TestMethod]
        public void FindWord_VerticalWordExists()
        {
            FoundWord foundWord = _characterGrid.FindWord(new Word(CAKE));

            Assert.IsTrue(foundWord != null);
            Assert.IsTrue(foundWord.A.x == 41 && foundWord.A.y == 4);
            Assert.IsTrue(foundWord.B.x == 41 && foundWord.A.y == 7);
        }

        [TestMethod]
        public void FindWord_LeftToRightDiagonalWordExists()
        {
            FoundWord foundWord = _characterGrid.FindWord(new Word(BAD_WOLF));

            Assert.IsTrue(foundWord != null);
            Assert.IsTrue(foundWord.A.x == 42 && foundWord.A.y == 11);
            Assert.IsTrue(foundWord.B.x == 36 && foundWord.A.y == 5);
        }

        [TestMethod]
        public void FindWord_RightToLeftDiagonalWordExists()
        {
            FoundWord foundWord = _characterGrid.FindWord(new Word(FOREST_CAKE));

            Assert.IsTrue(foundWord != null);
            Assert.IsTrue(foundWord.A.x == 12 && foundWord.A.y == 4);
            Assert.IsTrue(foundWord.B.x == 20 && foundWord.A.y == 12);
        }
    }
}
