using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            var numbers = lines.Select(l => long.Parse(l)).ToArray();

            int currentIndex = 24;
            bool validNumbersFound;
            do
            {
                currentIndex++;
                validNumbersFound = false;
                for (int firstNumberIndex = currentIndex - 25; firstNumberIndex < currentIndex - 1 && !validNumbersFound; firstNumberIndex++)
                {
                    for (int secondNumberIndex = firstNumberIndex + 1; secondNumberIndex < currentIndex; secondNumberIndex++)
                    {
                        if (numbers[firstNumberIndex] + numbers[secondNumberIndex] == numbers[currentIndex])
                        {
                            validNumbersFound = true;
                            break;
                        }
                    }
                }
            }
            while (validNumbersFound);

            int badIndex = currentIndex;
            long invalidNumber = numbers[badIndex];
            Console.WriteLine($"Unable to find two numbers that sum to {invalidNumber}.");

            for (int numberOfAddends = 2; numberOfAddends < currentIndex; numberOfAddends++)
            {
                for (currentIndex = 0; currentIndex < badIndex - numberOfAddends; currentIndex++)
                {
                    var addends = numbers.AsSpan(currentIndex, numberOfAddends).ToArray();
                    if (addends.Sum() == invalidNumber)
                    {
                        Console.WriteLine($"Sum found {string.Join(" + ", addends)} = {invalidNumber}");
                        Console.WriteLine($"{addends.Min()} + {addends.Max()} = {addends.Min() + addends.Max()}");
                    }
                }
            }
        }
    }
}
