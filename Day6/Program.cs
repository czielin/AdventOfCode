using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Day6
{
    class Program
    {
        private static string[] lines;

        static async Task Main(string[] args)
        {
            lines = await File.ReadAllLinesAsync("input.txt");
            Part1();
            Part2();
        }

        private static void Part1()
        {
            int sum = 0;

            HashSet<char> yesAnswers = new HashSet<char>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    sum += yesAnswers.Count;
                    yesAnswers = new HashSet<char>();
                }
                else
                {
                    foreach (char question in line)
                    {
                        if (!yesAnswers.Contains(question))
                        {
                            yesAnswers.Add(question);
                        }
                    }
                }
            }
            sum += yesAnswers.Count;
            Console.WriteLine($"The sum is {sum}.");
        }

        private static void Part2()
        {
            int sum = 0;

            HashSet<char> yesAnswers = new HashSet<char>();
            bool firstLine = true;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    sum += yesAnswers.Count;
                    yesAnswers = new HashSet<char>();
                    firstLine = true;
                }
                else if (firstLine)
                {
                    foreach (char question in line)
                    {
                        yesAnswers.Add(question);
                    }
                    firstLine = false;
                }
                else
                {
                    foreach (char question in yesAnswers)
                    {
                        if (!line.Contains(question))
                        {
                            yesAnswers.Remove(question);
                        }
                    }
                }
            }
            sum += yesAnswers.Count;
            Console.WriteLine($"The sum is {sum}.");
        }
    }
}
