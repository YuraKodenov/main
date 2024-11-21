using System;
using System.IO;

namespace Library
{
    public static class Lab3Runner
    {
        public static void Run(string inputFile, string outputFile)
        {
            // Перевіряємо наявність вхідного файлу
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Input file '{inputFile}' does not exist.");
                return;
            }

            // Читаємо дані з файлу
            string[] inputLines = File.ReadAllLines(inputFile);
            if (inputLines.Length == 0)
            {
                Console.WriteLine("Input file is empty.");
                return;
            }

            // Обробка першого рядка
            string[] firstLine = inputLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (firstLine.Length == 0)
            {
                Console.WriteLine("No data in the first line of input.");
                return;
            }

            // Перевірка другої лінії (якщо потрібно)
            string[] secondLine = inputLines.Length > 1 
                ? inputLines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                : Array.Empty<string>();

            // Обчислення результату (тут може бути специфічна логіка задачі)
            string result = CalculateShortestPath(firstLine, secondLine);

            // Записуємо результат у вихідний файл
            File.WriteAllText(outputFile, result);

            Console.WriteLine($"Result written to {outputFile}");
        }

        private static string CalculateShortestPath(string[] firstLine, string[] secondLine)
        {
            // Тут додаємо вашу специфічну логіку обчислень
            // Наприклад, повертаємо останнє число з першого рядка:
            return firstLine[^1]; // Останнє значення з першої лінії
        }
    }
}
