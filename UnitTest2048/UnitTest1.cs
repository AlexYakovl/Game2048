using TRPO;

namespace UnitTest2048
{
    public class UnitTest1
    {
        private MainPage CreateTestPageWithBoard(int[,] board)
        {
            var mainPage = new MainPage();
            mainPage.GetType().GetField("board", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(mainPage, board);
            return mainPage;
        }

        private int[,] GetBoard(MainPage page)
        {
            return (int[,])page.GetType().GetField("board", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(page);
        }



        [Fact]
        public void MoveRight()
        {
            var board = new int[4, 4]
            {
                { 2, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
            var mainPage = CreateTestPageWithBoard(board);

            var moveRightMethod = mainPage.GetType().GetMethod("MoveRight", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool moved = (bool)moveRightMethod.Invoke(mainPage, null);
            var updatedBoard = GetBoard(mainPage);

            Assert.True(moved);
            Assert.Equal(2, updatedBoard[0, 3]);
        }

        [Fact]
        public void MoveLeft()
        {
            var board = new int[4, 4]
            {
                { 0, 0, 0, 2 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
            var mainPage = CreateTestPageWithBoard(board);

            var moveLeftMethod = mainPage.GetType().GetMethod("MoveLeft", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool moved = (bool)moveLeftMethod.Invoke(mainPage, null);
            var updatedBoard = GetBoard(mainPage);

            Assert.True(moved);
            Assert.Equal(2, updatedBoard[0, 0]);
        }

        [Fact]
        public void MoveUp()
        {
            var board = new int[4, 4]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 2, 0, 0 }
            };
            var mainPage = CreateTestPageWithBoard(board);

            var moveUpMethod = mainPage.GetType().GetMethod("MoveUp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool moved = (bool)moveUpMethod.Invoke(mainPage, null);
            var updatedBoard = GetBoard(mainPage);

            Assert.True(moved);
            Assert.Equal(2, updatedBoard[0, 1]);
        }

        [Fact]
        public void MoveDown()
        {
            var board = new int[4, 4]
            {
                { 0, 2, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
            var mainPage = CreateTestPageWithBoard(board);

            var moveDownMethod = mainPage.GetType().GetMethod("MoveDown", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool moved = (bool)moveDownMethod.Invoke(mainPage, null);
            var updatedBoard = GetBoard(mainPage);

            Assert.True(moved);
            Assert.Equal(2, updatedBoard[3, 1]);
        }

        [Fact]
        public void AddRandomTile()
        {
            var board = new int[4, 4];
            var mainPage = CreateTestPageWithBoard(board);

            var addRandomTileMethod = mainPage.GetType().GetMethod("AddRandomTile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            addRandomTileMethod.Invoke(mainPage, null);
            var updatedBoard = GetBoard(mainPage);

            Assert.Single(updatedBoard.Cast<int>().Where(v => v == 2 || v == 4));
        }

        [Fact]
        public void HasEmptyCellsTRUE()
        {
            var board = new int[4, 4]
            {
                { 2, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
            var mainPage = CreateTestPageWithBoard(board);

            var hasEmptyCellsMethod = mainPage.GetType().GetMethod("HasEmptyCells", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool hasEmptyCells = (bool)hasEmptyCellsMethod.Invoke(mainPage, null);

            Assert.True(hasEmptyCells);
        }

        [Fact]
        public void HasEmptyCellsFALSE()
        {
            var board = new int[4, 4]
            {
                { 2, 2, 2, 2 },
                { 2, 2, 2, 2 },
                { 2, 2, 2, 2 },
                { 2, 2, 2, 2 }
            };
            var mainPage = CreateTestPageWithBoard(board);

            var hasEmptyCellsMethod = mainPage.GetType().GetMethod("HasEmptyCells", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool hasEmptyCells = (bool)hasEmptyCellsMethod.Invoke(mainPage, null);

            Assert.False(hasEmptyCells);
        }

        [Fact]
        public void TwoOrFour()
        {
            var mainPage = new MainPage();
            var twoOrFourMethod = mainPage.GetType().GetMethod("TwoOrFour", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int result = (int)twoOrFourMethod.Invoke(mainPage, null);

            Assert.Contains(result, new[] { 2, 4 });
        }

        [Fact]
        public void HasAvailableMovesTRUE()
        {
            var board = new int[4, 4]
            {
                { 2, 2, 4, 4 },
                { 8, 16, 32, 64 },
                { 128, 256, 512, 1024 },
                { 2048, 0, 4, 8 }
            };
            var mainPage = CreateTestPageWithBoard(board);

            var hasAvailableMovesMethod = mainPage.GetType().GetMethod("HasAvailableMoves", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool hasMoves = (bool)hasAvailableMovesMethod.Invoke(mainPage, null);

            Assert.True(hasMoves);
        }

        [Fact]
        public void HasAvailableMovesFALSE()
        {
            var board = new int[4, 4]
            {
                { 2, 4, 8, 16 },
                { 32, 64, 128, 256 },
                { 512, 1024, 2048, 4096 },
                { 8192, 16384, 32768, 65536 }
            };
            var mainPage = CreateTestPageWithBoard(board);

            var hasAvailableMovesMethod = mainPage.GetType().GetMethod("HasAvailableMoves", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool hasMoves = (bool)hasAvailableMovesMethod.Invoke(mainPage, null);

            Assert.False(hasMoves);
        }
    }
}