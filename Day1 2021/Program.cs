using System;
using System.IO;
using System.Threading.Tasks;

namespace Day1_2021
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            int previousMeasurement = int.MaxValue;
            int increases = 0;

            for (int lineCount = 0; lineCount < lines.Length; lineCount++)
            {
                int currentMeasurement = int.Parse(lines[lineCount]);
                if (currentMeasurement > previousMeasurement)
                {
                    increases++;
                }
                previousMeasurement = currentMeasurement;
            }

            Console.WriteLine($"The number of increases is: {increases}");
        }
    }
}
