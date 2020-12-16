using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ProcessFile("sample_input.txt", 2020);
            await ProcessFile("input.txt", 2020);

            await ProcessFile("sample_input.txt", 30000000);
            await ProcessFile("input.txt", 30000000);
        }

        private static async Task ProcessFile(string fileName, long numberToFind)
        {
            var numberList = (await File.ReadAllTextAsync(fileName)).Split(',').Select(s => int.Parse(s)).ToList();
            Dictionary<long, long> numbers = new Dictionary<long, long>();
            int index;
            for (index = 0; index < numberList.Count() - 1; index++)
            {
                numbers[numberList.ElementAt(index)] = index;
            }
            long lastNumber = numberList.Last();

            for (index = numbers.Count(); index < numberToFind; index++)
            {
                long newNumber = 0;
                if (numbers.TryGetValue(lastNumber, out long lastIndex))
                {
                    newNumber = index - lastIndex;
                }
                numbers[lastNumber] = index;
                lastNumber = newNumber;
            }

            Console.WriteLine($"The {numberToFind}th number in the sequence is {numbers.Single(kv => kv.Value == numberToFind - 1).Key}");
        }
    }
}
