using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ProcessFile("sample_input1.txt");
            await ProcessFile("sample_input2.txt");
            await ProcessFile("input.txt");
        }

        private static async Task ProcessFile(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);

            IEnumerable<int> adapterJolts = lines.Select(l => int.Parse(l)).OrderBy(j => j);

            Dictionary<int, int> joltDifferences = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 } };

            int previousJoltage = 0;
            foreach (int adapterJolt in adapterJolts)
            {
                int difference = adapterJolt - previousJoltage;
                joltDifferences[difference]++;
                previousJoltage = adapterJolt;
            }

            // Device difference.
            joltDifferences[3]++;

            Console.WriteLine($"1 jolt differences * 3 jolt differences: {joltDifferences[1] * joltDifferences[3]}");

            adapterJolts = adapterJolts.Prepend(0);
            adapterJolts = adapterJolts.Append(adapterJolts.Max() + 3);
            int previousJoltageIndex = 0;
            previousJoltage = 0;
            long totalPathCount = 1;
            for (int currentIndex = 1; currentIndex < adapterJolts.Count(); currentIndex++)
            {
                int currentJoltage = adapterJolts.ElementAt(currentIndex);
                if (currentJoltage - previousJoltage == 3)
                {
                    var section = adapterJolts.Skip(previousJoltageIndex).Take(currentIndex - previousJoltageIndex);
                    if (section.Count() > 1)
                    {
                        long pathCount = GetPathCount(section.ToArray(), 0);
                        totalPathCount *= pathCount;
                    }
                    previousJoltageIndex = currentIndex;
                }
                previousJoltage = currentJoltage;
            }

            Console.WriteLine($"Total adapter combinations: {totalPathCount}");
        }

        private static long GetPathCount(int[] jolts, int startIndex)
        {
            long pathCount = 0;
            int currentJolts = jolts[startIndex];

            for (int count = 1; count < 4; count++)
            {
                if (startIndex + count == jolts.Count())
                {
                    break;
                }
                int nextJolts = jolts[startIndex + count];
                if (nextJolts - currentJolts < 4)
                {
                    if (startIndex + count == jolts.Count() - 1)
                    {
                        pathCount++;
                    }
                    else
                    {
                        pathCount += GetPathCount(jolts, startIndex + count);
                    }
                }
            }
            return pathCount;
        }
    }
}
