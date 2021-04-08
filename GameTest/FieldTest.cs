using System.Linq;
using Game;
using Game.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTest
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var field = new Field();

            int initialEmptyCount = field.Width * field.Height;
            Assert.AreEqual(initialEmptyCount, CountEmptySquares(field));

            field.PlaceBalls();

            Assert.AreEqual(initialEmptyCount - field.BallsCount, CountEmptySquares(field));
        }

        [TestMethod]
        public void TestRemoveLinesVertical()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(1, 2), BallColor.White);
            field.SetBallColorAt(new Position(2, 2), BallColor.White);
            field.SetBallColorAt(new Position(3, 2), BallColor.White);
            field.SetBallColorAt(new Position(4, 2), BallColor.White);

            field.RemoveLines(new Position(2, 2));

            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(1, 2));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(2, 2));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(3, 2));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(4, 2));
        }

        [TestMethod]
        public void TestRemoveLinesHorizontal()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 0), BallColor.Red);
            field.SetBallColorAt(new Position(0, 1), BallColor.Red);
            field.SetBallColorAt(new Position(0, 2), BallColor.Red);
            field.SetBallColorAt(new Position(0, 3), BallColor.Red);
            field.SetBallColorAt(new Position(0, 4), BallColor.Red);

            field.RemoveLines(new Position(0, 0));

            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(0,0));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(0, 1));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(0, 2));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(0, 3));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(0, 4));
        }

        [TestMethod]
        public void TestRemoveLinesDescending()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 0), BallColor.Red);
            field.SetBallColorAt(new Position(1, 1), BallColor.Red);
            field.SetBallColorAt(new Position(2, 2), BallColor.Red);
            field.SetBallColorAt(new Position(3, 3), BallColor.Red);

            field.RemoveLines(new Position(0, 0));

            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(0, 0));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(1, 1));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(2, 2));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(3, 3));
        }

        [TestMethod]
        public void TestRemoveLinesAscending()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 3), BallColor.Yellow);
            field.SetBallColorAt(new Position(1, 2), BallColor.Yellow);
            field.SetBallColorAt(new Position(2, 1), BallColor.Yellow);
            field.SetBallColorAt(new Position(3, 0), BallColor.Yellow);

            field.RemoveLines(new Position(0, 3));

            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(0, 3));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(1, 2));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(2, 1));
            Assert.AreEqual(BallColor.Empty, field.GetBallColorAt(3, 0));
        }

        [TestMethod]
        public void TestNotRemoveLinesLessThan4()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(1, 2), BallColor.White);
            field.SetBallColorAt(new Position(2, 2), BallColor.White);
            field.SetBallColorAt(new Position(3, 2), BallColor.White);

            field.RemoveLines(new Position(2, 3));

            Assert.AreEqual(BallColor.White, field.GetBallColorAt(1, 2));
            Assert.AreEqual(BallColor.White, field.GetBallColorAt(2, 2));
            Assert.AreEqual(BallColor.White, field.GetBallColorAt(3, 2));
        }

        [TestMethod]
        public void TestGetPath1()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(1, 2), BallColor.White);

            var path = field.PathStrategy.GetPath(new Position(1, 2), new Position(3, 2), new bool[field.Height, field.Width]);

            var pathCount = path.Count;

            Assert.AreNotEqual(null, pathCount);
        }

        [TestMethod]
        public void TestGetPath2()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 1), BallColor.White);
            field.SetBallColorAt(new Position(1, 1), BallColor.White);
            field.SetBallColorAt(new Position(1, 0), BallColor.White);


            var path = field.PathStrategy.GetPath(new Position(0, 0), new Position(2, 2), new bool[field.Height, field.Width]);

            Assert.AreEqual(null, path);
        }

        [TestMethod]
        public void TestGetPath3()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 1), BallColor.White);
            field.SetBallColorAt(new Position(1, 1), BallColor.White);


            var path = field.PathStrategy.GetPath(new Position(0, 0), new Position(2, 2), new bool[field.Height, field.Width]);

            var pathCount = path.Count;

            Assert.AreNotEqual(null, pathCount);
        }

        [TestMethod]
        public void TestGetPath4()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 1), BallColor.White);
            field.SetBallColorAt(new Position(1, 0), BallColor.White);
            field.SetBallColorAt(new Position(1, 1), BallColor.White);
            field.SetBallColorAt(new Position(0, 0), BallColor.White);

            var path = field.PathStrategy.GetPath(new Position(0, 0), new Position(2, 2), new bool[field.Height, field.Width]);

            var pathCount = path.Count;

            Assert.AreEqual(null, pathCount);
        }

        [TestMethod]
        public void GetNeighbors1()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 0), BallColor.White);
            field.SetBallColorAt(new Position(0, 1), BallColor.White);
            field.SetBallColorAt(new Position(0, 2), BallColor.White);
            field.SetBallColorAt(new Position(1, 0), BallColor.White);
            field.SetBallColorAt(new Position(1, 2), BallColor.White);
            field.SetBallColorAt(new Position(2, 0), BallColor.White);
            field.SetBallColorAt(new Position(2, 1), BallColor.White);
            field.SetBallColorAt(new Position(2, 2), BallColor.White);

            var neighborList = field.GetNeighbors(new Position(1,1));

            var countNeighbors = neighborList.Count();
            Assert.AreEqual(4, countNeighbors);
        }

        [TestMethod]
        public void GetNeighbors2()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 1), BallColor.White);
            field.SetBallColorAt(new Position(1, 0), BallColor.White);
            field.SetBallColorAt(new Position(1, 1), BallColor.White);

            var neighborList = field.GetNeighbors(new Position(0, 0));

            var countNeighbors = neighborList.Count();
            Assert.AreEqual(2, countNeighbors);
        }

        [TestMethod]
        public void GetNeighbors3()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(7, 8), BallColor.White);
            field.SetBallColorAt(new Position(7, 7), BallColor.White);
            field.SetBallColorAt(new Position(8, 7), BallColor.White);

            var neighborList = field.GetNeighbors(new Position(8, 8));

            var countNeighbors = neighborList.Count();
            Assert.AreEqual(2, countNeighbors);

            Assert.AreEqual(neighborList[0], new Position(8, 7));
            Assert.AreEqual(neighborList[1], new Position(7, 8));
        }

        private static int CountEmptySquares(Field field)
        {
            int emptyCount = 0;

            for (int row = 0; row < field.Height; row++)
            {
                for (int col = 0; col < field.Width; col++)
                {
                    if (field.GetBallColorAt(row, col) == BallColor.Empty)
                    {
                        ++emptyCount;
                    }
                }
            }
            return emptyCount;
        }

        [TestMethod]
        public void TestGetPathWave1()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(1, 1), BallColor.White);

            var path = field.GetPathWave(new Position(1, 1), new Position(2, 2));
            var pathCount = path.Count;

            Assert.AreNotEqual(null, pathCount);
        }

        [TestMethod]
        public void TestGetPathWaveOriginal1()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(1, 1), BallColor.White);

            var pathDefault = field.PathStrategy.GetPath(new Position(1, 1), new Position(2, 3), new bool[field.Height, field.Width]);

            Assert.AreEqual(3, pathDefault.Count);

            IPathStrategy simpleStrategy = new GetPathSimple();

            var pathSimple = simpleStrategy.GetPath(new Position(1, 1), new Position(2, 3), new bool[field.Height, field.Width]);

            Assert.AreEqual(3, pathSimple.Count);
        }

        [TestMethod]
        public void TestGetPathWaveOriginal2()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 0), BallColor.White);

            var path = field.PathStrategy.GetPath(new Position(0, 0), new Position(5, 5), new bool[field.Height, field.Width]);

            Assert.AreNotEqual(null, path.Count);

            IPathStrategy simpleStrategy = new GetPathSimple();

            var pathSimple = simpleStrategy.GetPath(new Position(0, 0), new Position(5, 5), new bool[field.Height, field.Width]);

            Assert.AreNotEqual(null, pathSimple.Count);
        }

        [TestMethod]
        public void TestGetPathWaveOriginal3()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 0), BallColor.White);

            var path = field.PathStrategy.GetPath(new Position(0, 0), new Position(-1, -1), new bool[field.Height, field.Width]);

            Assert.AreEqual(null, path);

            IPathStrategy simpleStrategy = new GetPathSimple();

            var pathSimple = simpleStrategy.GetPath(new Position(0, 0), new Position(-1, -1), new bool[field.Height, field.Width]);

            Assert.AreEqual(null, pathSimple);
        }

        [TestMethod]
        public void TestGetPathWaveOriginal4()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 1), BallColor.White);
            field.SetBallColorAt(new Position(1, 0), BallColor.White);
            field.SetBallColorAt(new Position(1, 1), BallColor.White);
            field.SetBallColorAt(new Position(0, 0), BallColor.White);

            var path = field.PathStrategy.GetPath(new Position(0, 0), new Position(2, 2), new bool[field.Height, field.Width]);

            Assert.AreEqual(null, path);

            IPathStrategy simpleStrategy = new GetPathSimple();

            var pathSimple = simpleStrategy.GetPath(new Position(0, 0), new Position(2, 2), new bool[field.Height, field.Width]);

            Assert.AreEqual(null, pathSimple);

        }

        [TestMethod]
        public void TestGetPathWaveOriginal5()
        {
            var field = new Field();

            field.SetBallColorAt(new Position(0, 0), BallColor.Red);

            var path = field.PathStrategy.GetPath(new Position(0, 0), new Position(1, 1), new bool[field.Height, field.Width]);

            Assert.AreNotEqual(null, path);

            IPathStrategy simpleStrategy = new GetPathSimple();

            var pathSimple = simpleStrategy.GetPath(new Position(0, 0), new Position(1, 1), new bool[field.Height, field.Width]);

            Assert.AreNotEqual(null, pathSimple);

        }
    }
}
