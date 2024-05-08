namespace ChessKnightMoveCombinations
{
  class Program
  {
    static readonly char[] _vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };

    // Helper method to check if a cell is within board bounds
    static bool IsInBoardBounds(char[,] board, int row, int col) =>
      row >= 0 && row <= board.GetUpperBound(0) && col >= 0 && col <= board.GetUpperBound(1);

    // Class to represent a cell on the board with tracking for visited and vowel count
    class Cell
    {
      public int Row { get; set; }
      public int Col { get; set; }
      public bool Visited { get; set; }
      public bool IsVowel { get; private set; }

      public Cell(char[,] board, int row, int col)
      {
        Row = row;
        Col = col;
        IsVowel = _vowels.Contains(board[row, col]);
      }
    }

    // Recursive method to explore valid knight moves and count paths
    static long GetUniquePaths(char[,] board, Cell currentCell, int cellsTaken, int totalVowels)
    {
      if (!IsInBoardBounds(board, currentCell.Row, currentCell.Col))
      {
        return 0;
      }

      if (currentCell.Visited)
      {
        return 0;
      }

      currentCell.Visited = true;

      if (cellsTaken == board.GetLength(0) * board.GetLength(1)) // All cells visited
      {
        return 1;
      }

      long totalPaths = 0;
      int[] knightRowMoves = { 2, 2, -2, -2, 1, 1, -1, -1 };
      int[] knightColMoves = { 1, -1, 1, -1, 2, -2, 2, -2 };

      for (int i = 0; i < 8; i++)
      {
        int newRow = currentCell.Row + knightRowMoves[i];
        int newCol = currentCell.Col + knightColMoves[i];
        totalPaths += GetUniquePaths(board, new Cell(board, newRow, newCol), cellsTaken + 1, totalVowels + (currentCell.IsVowel ? 1 : 0));
      }

      currentCell.Visited = false;
      return totalPaths;
    }

    public static long SolveMatrix()
    {
      char[,] board =
      {
        { 'A', 'B', 'C', ' ', 'E' },
        { ' ', 'G', 'H', 'I', 'J' },
        { 'K', 'L', 'M', 'N', 'O' },
        { 'P', 'Q', 'R', 'S', 'T' },
        { 'U', 'V', ' ', ' ', 'Y' },
      };

      int row = 1;
      int col = 2;
      if (!IsInBoardBounds(board, row, col))
      {
        Console.WriteLine("Invalid row/column given");
        return 0;
      }

      Console.WriteLine($"Starting Position: {board[row, col]}");

      var startCell = new Cell(board, row, col);
      return GetUniquePaths(board, startCell, 1, startCell.IsVowel ? 1 : 0);
    }
  }
}
