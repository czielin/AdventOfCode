using System;
using System.IO;
using System.Threading.Tasks;

namespace Day2_2021
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            int horizontalPosition = 0;
            int depth = 0;

            for (int lineCount = 0; lineCount < lines.Length; lineCount++)
            {
                var lineParts = lines[lineCount].Split(' ');
                string action = lineParts[0];
                int units = int.Parse(lineParts[1]);

                switch (action)
                {
                    case "forward":
                        horizontalPosition += units;
                        break;
                    case "down":
                        depth += units;
                        break;
                    case "up":
                        depth -= units;
                        break;
                }
            }

            Console.WriteLine($"Depth * horizontal position: {depth * horizontalPosition}");

            horizontalPosition = 0;
            depth = 0;
            int aim = 0;

            for (int lineCount = 0; lineCount < lines.Length; lineCount++)
            {
                var lineParts = lines[lineCount].Split(' ');
                string action = lineParts[0];
                int units = int.Parse(lineParts[1]);

                switch (action)
                {
                    case "forward":
                        horizontalPosition += units;
                        depth += aim * units;
                        break;
                    case "down":
                        aim += units;
                        break;
                    case "up":
                        aim -= units;
                        break;
                }
            }

            Console.WriteLine($"Depth * horizontal position: {depth * horizontalPosition}");
        }
    }
}
