using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    private static int[,] baseLayout = null!; // Suppress nullable warning by asserting non-null assignment
    private static int N, M;
    private static (int x, int y) agentStart;
    private static List<(int x, int y)> exits = new List<(int x, int y)>();
    private static Dictionary<(int x, int y), (int x, int y)> tunnels = new Dictionary<(int x, int y), (int x, int y)>();

    public static void Main()
    {
        string inputPath = @"C:\Users\yurka\OneDrive\Рабочий стол\KPP\Lab3\INPUT.TXT";
        string outputPath = @"C:\Users\yurka\OneDrive\Рабочий стол\KPP\Lab3\OUTPUT.TXT";

        if (!File.Exists(inputPath))
        {
            File.WriteAllText(outputPath, "Impossible");
            return;
        }

        try
        {
            ParseInput(inputPath);
            int result = FindShortestEscapePath();

            if (result == -1)
                File.WriteAllText(outputPath, "Impossible");
            else
                File.WriteAllText(outputPath, result.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            File.WriteAllText(outputPath, "Impossible");
        }
    }

    private static void ParseInput(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        
        if (lines.Length < 3)
            throw new Exception("Input file format is incorrect: insufficient lines for dimensions and agent position.");

        // Parse dimensions
        string[] dimensions = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (dimensions.Length < 2)
            throw new Exception("Input file format is incorrect: dimensions missing.");

        N = int.Parse(dimensions[0]);
        M = int.Parse(dimensions[1]);

        // Initialize base layout with parsed dimensions
        baseLayout = new int[N, M];

        // Parse agent start position
        string[] startPosition = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (startPosition.Length < 2)
            throw new Exception("Input file format is incorrect: agent start position missing.");

        agentStart = (int.Parse(startPosition[0]) - 1, int.Parse(startPosition[1]) - 1);

        // Parse base layout
        for (int i = 0; i < N; i++)
        {
            string[] row = lines[i + 2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (row.Length < M)
                throw new Exception($"Input file format is incorrect: insufficient columns in row {i + 2}.");

            for (int j = 0; j < M; j++)
            {
                baseLayout[i, j] = int.Parse(row[j]);
            }
        }

        // Parse tunnels
        int tunnelCount = int.Parse(lines[N + 2].Trim());
        for (int i = 0; i < tunnelCount; i++)
        {
            string[] tunnelInfo = lines[N + 3 + i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tunnelInfo.Length < 4)
                throw new Exception($"Input file format is incorrect: insufficient tunnel data on line {N + 3 + i}.");

            var entry = (int.Parse(tunnelInfo[0]) - 1, int.Parse(tunnelInfo[1]) - 1);
            var exit = (int.Parse(tunnelInfo[2]) - 1, int.Parse(tunnelInfo[3]) - 1);
            tunnels[entry] = exit;
        }

        // Parse exits
        int exitCount = int.Parse(lines[N + 3 + tunnelCount].Trim());
        for (int i = 0; i < exitCount; i++)
        {
            string[] exitInfo = lines[N + 4 + tunnelCount + i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (exitInfo.Length < 2)
                throw new Exception($"Input file format is incorrect: insufficient exit data on line {N + 4 + tunnelCount + i}.");

            exits.Add((int.Parse(exitInfo[0]) - 1, int.Parse(exitInfo[1]) - 1));
        }
    }

    private static int FindShortestEscapePath()
    {
        var directions = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var visited = new HashSet<(int, int)>();
        var queue = new Queue<((int x, int y) position, int distance)>();

        queue.Enqueue((agentStart, 0));
        visited.Add(agentStart);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var (x, y) = current.position;

            if (exits.Contains((x, y)))
                return current.distance;

            foreach (var direction in directions)
            {
                int nx = x + direction.x, ny = y + direction.y;

                if (nx >= 0 && nx < N && ny >= 0 && ny < M && baseLayout[nx, ny] == 0 && !visited.Contains((nx, ny)))
                {
                    visited.Add((nx, ny));
                    queue.Enqueue(((nx, ny), current.distance + 1));
                }
            }

            // Check for hyper-tunnel
            if (tunnels.ContainsKey((x, y)) && !visited.Contains(tunnels[(x, y)]))
            {
                var tunnelExit = tunnels[(x, y)];
                visited.Add(tunnelExit);
                queue.Enqueue((tunnelExit, current.distance + 1));
            }
        }

        return -1;
    }
}
