using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("sample_input.txt");

            var cups = lines[0].Select(l => int.Parse(l.ToString())).ToList();

            var part1 = MoveCups(cups, 100);

            Console.WriteLine($"The current cup order is: {string.Join("", part1)}");

            for (int count = cups.Max() + 1; count <= 1000000; count++)
            {
                cups.Add(count);
            }

            var part2 = MoveCups(cups, 10000000);

            Console.WriteLine($"The current cup order is: {string.Join("", part2)}");
        }

        private static List<int> MoveCups(List<int> originalCups, int numberOfMoves)
        {
            List<int> cups = originalCups.ToList();
            int currentCup = cups.ElementAt(0);

            for (int moves = 1; moves <= numberOfMoves; moves++)
            {
                int currentCupIndex = cups.IndexOf(currentCup);
                //Console.WriteLine($"Move {moves}");
                //Console.WriteLine($"Current cup value: {currentCup}");
                //Console.WriteLine($"Current cup position: {currentCupIndex + 1}");
                //Console.WriteLine($"Current cups: {string.Join(", ", cups)}");
                List<int> pickupCups = new List<int>();
                for (int pickupCount = 1; pickupCount <= 3; pickupCount++)
                {
                    int pickupCup = cups[(currentCupIndex + pickupCount) % cups.Count()];
                    pickupCups.Add(pickupCup);
                }
                //Console.WriteLine($"Pickup cups: {string.Join(", ", pickupCups)}");
                int destinationCup = currentCup - 1;
                cups.RemoveAll(c => pickupCups.Contains(c));
                while (!cups.Contains(destinationCup))
                {
                    destinationCup--;
                    if (destinationCup < cups.Min())
                    {
                        destinationCup = cups.Max();
                    }
                }
                int destinationCupIndex = cups.IndexOf(destinationCup);
                //Console.WriteLine($"Destination: {destinationCup}");
                cups.InsertRange(destinationCupIndex + 1, pickupCups);
                currentCup = cups[(cups.IndexOf(currentCup) + 1) % cups.Count()];
                //Console.WriteLine();
            }

            return cups;
        }
    }
}
