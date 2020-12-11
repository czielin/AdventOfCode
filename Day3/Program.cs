using System;
using System.IO;
using System.Threading.Tasks;

namespace Day3
{
    class Program
    {
        private static string[] lines;

        static async Task Main(string[] args)
        {
            lines = await File.ReadAllLinesAsync("input.txt");
            long product = TreeCount(1, 1);
            product *= TreeCount(3, 1);
            product *= TreeCount(5, 1);
            product *= TreeCount(7, 1);
            product *= TreeCount(1, 2);
            Console.WriteLine($"The product of all paths is {product}.");
        }

        private static long TreeCount(int right, int down)
        {
            int column = 0;
            int totalColumns = lines[0].Length;
            int trees = 0;

            for (int lineCount = down; lineCount < lines.Length; lineCount += down)
            {
                column = (column + right) % totalColumns;
                if (lines[lineCount][column] == '#')
                {
                    trees++;
                }
            }

            Console.WriteLine($"Encountered {trees} trees for Right {right}, Down {down}.");

            return trees;
        }
    }
}
