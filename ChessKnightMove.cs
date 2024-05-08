public class KannappanVenkatachalam
{
    public static int N; // Board size (replace constant with a variable)

    public static readonly int[] row = { 2, 1, -1, -2, -2, -1, 1, 2, 2 };
    public static readonly int[] col = { 1, 2, 2, 1, -1, -2, -2, -1, 1 };

    private static bool IsValid(int x, int y, char[,] board)
    {
        return x >= 0 && y >= 0 && x < N && y < N && board[y, x] != ' ';
    }

    public static int SolveMatrix(int x, int y, int pos, int vowelCount, char[,] board)
    {
        int key = x * N + y;

        // Base case: all squares visited
        if (pos == N * N)
        {
            return 1; // One unique path found
        }

        int uniquePaths = 0;
        for (int k = 0; k < 8; k++)
        {
            int newX = x + row[k];
            int newY = y + col[k];

            // Skip invalid or visited positions
            if (!IsValid(newX, newY, board))
            {
                continue;
            }

            char cellChar = board[newY, newX];
            if (vowelCount == 2 && "AEIOUY".Contains(cellChar))
            {
                continue;
            }

            uniquePaths += SolveMatrix(newX, newY, pos + 1, cellChar == 'Y' ? vowelCount + 1 : vowelCount, board);
        }

        return uniquePaths;
    }

    public static void Main(string[] args)
    {
        char[,] board = new char[N, N]
        {
            { 'A', 'B', 'C', 'E', ' ' }, // Replace space with a valid character
            { ' ', 'G', 'H', 'I', 'J' },
            { 'K', 'L', 'M', 'N', 'O' },
            { 'P', 'Q', 'R', 'S', 'T' },
            { 'U', 'V', ' ', ' ', 'Y' }  // Replace spaces with valid characters
        };


        int uniquePaths = SolveMatrix(0, 0, 1, 0, board);
        Console.WriteLine("Number of unique paths with 8 moves and at most 2 vowels: " + uniquePaths);
    }
}