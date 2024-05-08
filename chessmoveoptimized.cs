namespace ChessKnightMoveCombinations
{
  class Program
  {
    static readonly char[] _vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };

    // Helper method to check if a cell is within board bounds
    static bool IsInBoardBounds(char[,] board, int row, int col) =>
      row >= 0 && row <= board.GetUpperBound(0) && col >= 0 && col <= board.GetUpperBound(1);

    // Struct to represent a cell on the board with coordinates
    struct Cell
    {
      public int Row { get; }
      public int Col { get; }

      public Cell(int row, int col)
      {
        Row = row;
        Col = col;
      }
    }

    // Delegate to define the callback function for processing paths (e.g., counting)
    public delegate void PathProcessor(Stack<Cell> path, int totalVowels);

    // Recursive method to explore valid knight moves and process paths
    static void GetUniquePaths(char[,] board, Stack<Cell> path, int totalVowels, PathProcessor processor)
    {
      if (path.Count == board.GetLength(0) * board.GetLength(1)) // All cells visited
      {
        processor(path, totalVowels);
        return;
      }

      Cell currentCell = path.Peek();

      if (!IsInBoardBounds(board, currentCell.Row, currentCell.Col))
      {
        return;
      }

      int[] knightRowMoves = { 2, 2, -2, -2, 1, 1, -1, -1 };
      int[] knightColMoves = { 1, -1, 1, -1, 2, -2, 2, -2 };

      foreach (var move in Enumerable.Range(0, 8).Zip(knightRowMoves, knightColMoves))
      {
        int newRow = currentCell.Row + move.Second;
        int newCol = currentCell.Col + move.First;
        if (IsInBoardBounds(board, newRow, newCol))
        {
          path.Push(new Cell(newRow, newCol));
          GetUniquePaths(board, path, totalVowels + (_vowels.Contains(board[newRow, newCol]) ? 1 : 0), processor);
          path.Pop();
        }
      }
    }

    public static void SolveMatrix(char[,] board, int row, int col, Action<long> pathCountCallback)
    {
      if (!IsInBoardBounds(board, row, col))
      {
        Console.WriteLine("Invalid row/column given");
        return;
      }

      Console.WriteLine($"Starting Position: {board[row, col]}");

      var startCell = new Cell(row, col);
      var path = new Stack<Cell>(new[] { startCell });

      void ProcessPath(Stack<Cell> pathStack, int vowelCount)
      {
        // You can modify this logic to perform desired actions on each path
        // For example, to count paths:
        pathCountCallback(pathStack.Count);
      }

      GetUniquePaths(board, path, _vowels.Contains(board[row, col]) ? 1 : 0, ProcessPath);
    }
  }
}
