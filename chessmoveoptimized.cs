namespace ChessKnightMoveCombinations
{
  class Program
  {
    // ... rest of your code ...

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
  // Example usage
char[,] myBoard = {{ ... }};
int startingRow = 1;
int startingCol = 2;

// Define your callback function (optional)
void PrintPathCount(long count)
{
  Console.WriteLine($"Total Paths: {count}");
}

// Call SolveMatrix with the callback function (or a null reference if not needed)
Program.SolveMatrix(myBoard, startingRow, startingCol, PrintPathCount); // Or Program.SolveMatrix(myBoard, startingRow, startingCol, null);
}
