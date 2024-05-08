Console.WriteLine($"Paths: {ChessKnightMoveCombinations.Program.SolveMatrix()}");

namespace ChessKnightMoveCombinations
{
    class Program
    {
        static readonly char[] _vowels = {
            'A', 'E', 'I', 'O', 'U', 'Y'
        };

        /// <summary>
        /// Checks if the given bounds of the board are valid
        /// </summary>
        /// <param name="board">The board containing a matrix of letters</param>
        /// <param name="row">The row to validate</param>
        /// <param name="col">The column to validate</param>
        /// <returns>A boolean representing if the index given to the board is valid</returns>
        static bool IsInBoardBounds(char[,] board, int row, int col)
        {
            int boardRowLastIndex = board.GetUpperBound(0);
            int boardColumnLastIndex = board.GetUpperBound(1);
            return row <= boardRowLastIndex && col <= boardColumnLastIndex &&
                row >= 0 && col >= 0;
        }

        /// <summary>
        /// Checks if a given move will be valid.
        /// </summary>
        /// <remarks>Does not validate if the move was a "knight" move; just if the restrictions given will keep the path valid</remarks>
        /// <param name="board">The board containing a matrix of letters</param>
        /// <param name="row">The row to validate</param>
        /// <param name="col">The column to validate</param>
        /// <param name="totalVowels">Total count of vowels in the current path</param>
        /// <param name="lastRow">The last row index in the path</param>
        /// <param name="lastCol">The last column index in the path</param>
        /// <returns>A boolean represnting if the given move is valid</returns>
        static bool IsMoveValid(char[,] board, int row, int col, int totalVowels, int lastRow, int lastCol)
        {
            char value = board[row, col];
            if (value == ' ') return false;
            if (row == lastRow && col == lastCol) return false;
            if (IsVowel(board, row, col))
            {
                totalVowels++;
                if (totalVowels > 2) return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the given index of the board is a vowel
        /// </summary>
        /// <param name="board">The board containing a matrix of letters</param>
        /// <param name="row">The row to validate</param>
        /// <param name="col">The column to validate</param>
        /// <returns>A boolean representing if the given character on the board is a vowel including 'Y'</returns>
        static bool IsVowel(char[,] board, int row, int col)
        {
            return Array.Exists(_vowels, v => v == board[row, col]);
        }

        /// <summary>
        /// Finishes computing the unique paths by the given "knight" move
        /// </summary>
        /// <param name="board">The board containing a matrix of letters</param>
        /// <param name="newRow">The new row from the knights move</param>
        /// <param name="newCol">The new column from the knights move</param>
        /// <param name="cellsTaken">Current count of cells in the path</param>
        /// <param name="totalVowels">Current count of vowels in the path</param>
        /// <param name="lastRow">The last row from the knights move</param>
        /// <param name="lastCol">The last column from the knights move</param>
        /// <param name="currentRow">The current row calculated from</param>
        /// <param name="currentCol">The current column calculated from</param>
        /// <returns>A 64 bit integer containing the amount of unique paths found</returns>
        static long FinishPath(char[,] board, int newRow, int newCol, int cellsTaken, int totalVowels, int lastRow,
            int lastCol, int currentRow, int currentCol)
        {
            if (!IsInBoardBounds(board, newRow, newCol)) return 0;

            if (IsMoveValid(board, newRow, newCol, totalVowels, lastRow, lastCol))
            {
                int nextCellCount = cellsTaken + 1;
                return GetUniquePaths(board, newRow, newCol, nextCellCount, totalVowels, currentRow, currentCol);
            }

            return 0;
        }

        /// <summary>
        /// An algorithm that computes the solution to the "Chess Knight Move Combinations" exercise
        /// </summary>
        /// <param name="board">The board containing a matrix of letters</param>
        /// <param name="row">The row to start from in the board</param>
        /// <param name="col">The column to start from in the board</param>
        /// <param name="cellsTaken">The cells in the path (Likely to start with 1)</param>
        /// <param name="totalVowels">The vowel count in the path</param>
        /// <param name="lastRow">The last row calculated from (Likely to start at -1)</param>
        /// <param name="lastCol">The last column calclulated from (Likely to start at -1)</param>
        /// <returns>A 64 bit integer containing the amount of unique paths found</returns>
        static long GetUniquePaths(char[,] board, int row, int col, int cellsTaken, int totalVowels, int lastRow, int lastCol)
        {
            long totalPaths = 0;

            if (IsVowel(board, row, col)) totalVowels++;
            const int MAX_CELLS = 8;
            if (cellsTaken == MAX_CELLS)
            {
                return 1;
            }

            int newRow = row + 2;
            int newCol = col + 1;
            totalPaths += FinishPath(board, newRow, newCol, cellsTaken, totalVowels, lastRow, lastCol, row, col);

            newRow = row + 2;
            newCol = col - 1;
            totalPaths += FinishPath(board, newRow, newCol, cellsTaken, totalVowels, lastRow, lastCol, row, col);

            newRow = row - 2;
            newCol = col + 1;
            totalPaths += FinishPath(board, newRow, newCol, cellsTaken, totalVowels, lastRow, lastCol, row, col);

            newRow = row - 2;
            newCol = col - 1;
            totalPaths += FinishPath(board, newRow, newCol, cellsTaken, totalVowels, lastRow, lastCol, row, col);

            newRow = row + 1;
            newCol = col + 2;
            totalPaths += FinishPath(board, newRow, newCol, cellsTaken, totalVowels, lastRow, lastCol, row, col);

            newRow = row - 1;
            newCol = col + 2;
            totalPaths += FinishPath(board, newRow, newCol, cellsTaken, totalVowels, lastRow, lastCol, row, col);

            newRow = row + 1;
            newCol = col - 2;
            totalPaths += FinishPath(board, newRow, newCol, cellsTaken, totalVowels, lastRow, lastCol, row, col);

            newRow = row - 1;
            newCol = col - 2;
            totalPaths += FinishPath(board, newRow, newCol, cellsTaken, totalVowels, lastRow, lastCol, row, col);

            return totalPaths;
        }

        public static long SolveMatrix()
        {
            char[,] board = {
                { 'A', 'B', 'C', ' ', 'E' },
                { ' ', 'G', 'H', 'I', 'J' },
                { 'K', 'L', 'M', 'N', 'O' },
                { 'P', 'Q', 'R', 'S', 'T' },
                { 'U', 'V', ' ', ' ', 'Y' },
            };

            int boardRowLength = board.GetUpperBound(0) + 1;
            int boardColumnLength = board.GetUpperBound(1) + 1;

            // Drawing board to console
            for (int i = 0; i < boardRowLength; i++)
            {
                for (int j = 0; j < boardColumnLength; j++)
                {
                    Console.Write(board[i, j]);
                }

                Console.WriteLine();
            }

            int row = 1;
            int col = 2;
            if (!IsInBoardBounds(board, row, col))
            {
                Console.WriteLine("Invalid row/column given");
                return 0;
            }
            Console.WriteLine($"Starting Position: {board[row, col]}");

            // Start of algorithm computation
            if (IsVowel(board, row, col))
            {
                return GetUniquePaths(board, row, col, 1, 1, -1, -1);
            }
            else
            {
                return GetUniquePaths(board, row, col, 1, 0, -1, -1);
            }
        }
    }
}