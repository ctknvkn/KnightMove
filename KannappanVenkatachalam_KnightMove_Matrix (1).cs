using System;
using System.Collections.Generic;

public class KannappanVenkatachalam
{
  // `N × N` chessboard
  public static readonly int N = 5;

  public static readonly int[] row = { 2, 1, -1, -2, -2, -1, 1, 2, 2 };// Valid horizontal moves
  public static readonly int[] col = { 1, 2, 2, 1, -1, -2, -2, -1, 1 };// valid vertical moves

  // Check if `(row, column)` is valid chessboard coordinate
  // Note that a knight cannot go out of the chessboard
  private static bool IsValid(int x, int y)
  {
    if (x < 0 || y < 0 || x >= N || y >= N)
    {
      return false;
    }

    return true;
  }

  private static void Print(int[,] visited)
  {
    for (int r = 0; r < N; r++)
    {
      Console.WriteLine(string.Join(", ", visited[r, 0], visited[r, 1], visited[r, 2], visited[r, 3], visited[r, 4]));
    }

    Console.WriteLine();
  }

  public static void SolveMatrix(HashSet<int> visited, int x, int y, int pos)
  {
    // Convert coordinates (x, y) to a single unique value for the HashSet
    int key = x * N + y;

    visited.Add(key);

    // if all squares are visited, print the solution
    if (visited.Count == N * N)
    {
      Print(ConvertVisitedArray(visited)); // Helper function to convert back

      // backtrack before returning
      visited.Remove(key);
      return;
    }

    for (int k = 0; k < 8; k++)
    {
      int newX = x + row[k];
      int newY = y + col[k];

      // if the new position is valid and not visited yet
      if (IsValid(newX, newY) && !visited.Contains(newX * N + newY))
      {
        SolveMatrix(visited, newX, newY, pos + 1);
      }
    }

    visited.Remove(key);
  }

  private static int[,] ConvertVisitedArray(HashSet<int> visited)
{
  int[,] result = new int[N, N];
  int counter = 1;

  // Iterate through visited set and directly populate result array
  foreach (int key in visited)
  {
    int x = key / N;
    int y = key % N;
    result[x, y] = counter++;
  }

  return result;
}

  public static void Main(string[] args)
  {
    HashSet<int> visited = new HashSet<int>();

    int pos = 1;

    // start knight tour from corner square `(0, 0)`
    SolveMatrix(visited, 0, 0, pos);
  }
}
