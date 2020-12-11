using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Part1();
            await Part2();
        }

        private static async Task Part1()
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            int[] numbers = lines.Select(l => int.Parse(l)).ToArray();
            for (int count1 = 0; count1 < numbers.Length; count1++)
            {
                for (int count2 = 0; count2 < numbers.Length; count2++)
                {
                    if (numbers[count1] + numbers[count2] == 2020)
                    {
                        Console.WriteLine($"Found: {numbers[count1]} + {numbers[count2]} = 2020");
                        break;
                    }
                }
            }
        }

        private static async Task Part2()
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            int[] numbers = lines.Select(l => int.Parse(l)).ToArray();
            for (int count1 = 0; count1 < numbers.Length; count1++)
            {
                for (int count2 = 0; count2 < numbers.Length; count2++)
                {
                    for (int count3 = 0; count3 < numbers.Length; count3++)
                    {
                        if (numbers[count1] + numbers[count2] + numbers[count3] == 2020)
                        {
                            Console.WriteLine($"Found: {numbers[count1]} + {numbers[count2]} + {numbers[count3]} = 2020");
                            Console.WriteLine($"Product: {numbers[count1] * numbers[count2] * numbers[count3]}");
                            break;
                        }
                    }
                }
            }
        }
    }
}
