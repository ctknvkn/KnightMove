using System;

public class ChessKnightMoveCombinations
{
    private static readonly int[] _rowMoves = { 2, 1, -1, -2, -2, -1, 1, 2 };
    private static readonly int[] _colMoves = { 1, 2, 2, 1, -1, -2, -2, -1 };
    private static readonly char[] _vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };

    public static long SolveMatrix(char[,] board, int row, int col)
    {
        if (!IsInBounds(board, row, col))
        {
            return 0;
        }

        return GetUniquePaths(board, row, col, 0, 0);
    }

    private static bool IsInBounds(char[,] board, int row, int col)
    {
        return row >= 0 && row < board.GetLength(0) && col >= 0 && col < board.GetLength(1);
    }

    private static bool IsVowel(char[,] board, int row, int col)
    {
        return Array.IndexOf(_vowels, board[row, col]) != -1;
    }

    private static long GetUniquePaths(char[,] board, int row, int col, int cellsTaken, int totalVowels)
    {
        if (cellsTaken == 8)
        {
            return 1;
        }

        long totalPaths = 0;
        for (int i = 0; i < 8; i++)
        {
            int newRow = row + _rowMoves[i];
            int newCol = col + _colMoves[i];

            if (IsInBounds(board, newRow, newCol))
            {
                char cellValue = board[newRow, newCol];
                if (cellValue != ' ' && (totalVowels < 2 || !IsVowel(board, newRow, newCol)))
                {
                    totalPaths += GetUniquePaths(board, newRow, newCol, cellsTaken + 1, cellValue == 'Y' ? totalVowels + 1 : totalVowels);
                }
            }
        }

        return totalPaths;
    }
}
