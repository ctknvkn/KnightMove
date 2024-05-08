using System;

namespace KannappanVenkatachalam
{
    class Program
    {
        static readonly char[] _vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };

        static bool IsInBoardBounds(char[,] board, int row, int col)
        {
            return row >= 0 && col >= 0 && row < board.GetLength(0) && col < board.GetLength(1);
        }

        static bool IsVowel(char c)
        {
            return Array.IndexOf(_vowels, c) >= 0;
        }

        static long FinishPath(char[,] board, int newRow, int newCol, int cellsTaken, ref int totalVowels, int currentRow, int currentCol)
        {
            if (!IsInBoardBounds(board, newRow, newCol)) return 0;
            char value = board[newRow, newCol];
            if (value == ' ' || (newRow == currentRow && newCol == currentCol)) return 0;
            if (IsVowel(value))
            {
                if (++totalVowels > 2) return 0;
            }
            return GetUniquePaths(board, newRow, newCol, cellsTaken + 1, ref totalVowels);
        }

        static long GetUniquePaths(char[,] board, int row, int col, int cellsTaken, ref int totalVowels)
        {
            long totalPaths = 0;
            const int MAX_CELLS = 8;
            if (cellsTaken == MAX_CELLS) return 1;

            // Possible knight moves
            int[,] moves = { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } };

            for (int i = 0; i < moves.GetLength(0); i++)
            {
                int newRow = row + moves[i, 0];
                int newCol = col + moves[i, 1];
                int currentVowels = totalVowels; // Copy current vowel count to avoid side-effects
                totalPaths += FinishPath(board, newRow, newCol, cellsTaken, ref currentVowels, row, col);
            }

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

            Console.WriteLine("Board:");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }

            int row = 1, col = 2;
            if (!IsInBoardBounds(board, row, col))
            {
                Console.WriteLine("Invalid row/column given");
                return 0;
            }
            Console.WriteLine($"Starting Position: {board[row, col]}");

            int initialVowels = IsVowel(board[row, col]) ? 1 : 0;
            return GetUniquePaths(board, row, col, 1, ref initialVowels);
        }

        public static void Main()
        {
            SolveMatrix();
        }
    }
    
}
