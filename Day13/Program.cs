using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ProcessFile("sample_input1.txt");
            await ProcessFile("sample_input2.txt");
            await ProcessFile("sample_input3.txt");
            await ProcessFile("sample_input4.txt");
            await ProcessFile("sample_input5.txt");
            await ProcessFile("input.txt");
        }

        private static async Task ProcessFile(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);
            long earliestDepartureTime = long.Parse(lines[0]);
            var busLines = lines[1].Split(',').Select(b => long.TryParse(b, out long bInt) ? bInt : 0).ToArray();
            long earliestBusTime = long.MaxValue;
            long earliestBusLine = 0;
            long largestBusInterval = 0;
            long largestBusIntervalIndex = 0;
            long index = 0;

            for (index = 0; index < busLines.Length; index++)
            {
                long busLine = busLines[index];
                if (busLine > 0)
                {
                    long departureTime = busLine - (earliestDepartureTime % busLine);
                    if (departureTime < earliestBusTime)
                    {
                        earliestBusTime = departureTime;
                        earliestBusLine = busLine;
                    }
                    if (busLine > largestBusInterval)
                    {
                        largestBusInterval = busLine;
                        largestBusIntervalIndex = index;
                    }
                }
            }

            Console.WriteLine($"The earliest bus that can be taken is {earliestBusLine} and will require waiting {earliestBusTime} minutes.");
            Console.WriteLine($"{earliestBusLine} x {earliestBusTime} = {earliestBusLine * earliestBusTime}");

            // Calculate by solving system of modular equations and Chinese Remainder Theorem.
            long mSum = 0;

            for (int busLineIndex = 0; busLineIndex < busLines.Length; busLineIndex++)
            {
                long busLine = busLines[busLineIndex];
                if (busLine != 0)
                {
                    long m = busLines.Where(b => b != 0 && b != busLine).Aggregate((i, j) => i * j);
                    long mInverse = FindInverse(m, busLine);
                    mSum += (busLine - busLineIndex) * m * mInverse;
                }
            }
            long product = busLines.Where(b => b != 0).Aggregate((i, j) => i * j);

            long timestamp = mSum % product;

            Console.WriteLine($"The timestamp is {timestamp}.");
        }

        private static long FindInverse(long value, long mod)
        {
            for (long candidate = 1; candidate < mod; candidate++)
            {
                if ((candidate * value) % mod == 1)
                {
                    return candidate;
                }
            }
            throw new Exception("Inverse not found");
        }
    }
}
