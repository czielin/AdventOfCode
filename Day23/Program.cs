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
            var lines = await File.ReadAllLinesAsync("input.txt");

            var cups = lines[0].Select(l => int.Parse(l.ToString())).ToList();

            //var part1 = MoveCups(cups, 100);

            //Console.WriteLine($"The current cup order is: {string.Join("", part1)}");

            Cup cupOne = MoveCupsFaster(cups, 100);

            Console.Write($"The current cup order is: ");

            Cup currentCup = cupOne.Next;
            while (currentCup != cupOne)
            {
                Console.Write(currentCup.Value);
                currentCup = currentCup.Next;
            }
            Console.WriteLine();

            for (int count = cups.Max() + 1; count <= 1000000; count++)
            {
                cups.Add(count);
            }

            //var part2 = MoveCups(cups, 10000000);

            //Console.WriteLine($"The current cup order is: {string.Join("", part2)}");

            cupOne = MoveCupsFaster(cups, 10000000);

            Console.WriteLine($"The part 2 product is: { ((long)cupOne.Next.Value) * ((long)cupOne.Next.Next.Value) }");
        }

        // Second attempt to calculate part 2 faster.
        private static Cup MoveCupsFaster(List<int> originalCups, int numberOfMoves)
        {
            Dictionary<int, Cup> cups = new Dictionary<int, Cup>();
            Cup previousCup = null;
            foreach (int cupValue in originalCups)
            {
                Cup cup = new Cup { Value = cupValue };
                cups.Add(cupValue, cup);
                if (previousCup != null)
                {
                    cup.Previous = previousCup;
                    previousCup.Next = cup;
                }
                previousCup = cup;
            }
            Cup firstCup = cups[originalCups.First()];
            firstCup.Previous = previousCup;
            previousCup.Next = firstCup;

            Cup currentCup = firstCup;
            int minValue = originalCups.Min();
            int maxValue = originalCups.Max();
            for (int moves = 1; moves <= numberOfMoves; moves++)
            {
                Cup nextCup = currentCup.Next;
                currentCup.Next = currentCup.Next.Next.Next.Next;
                currentCup.Next.Previous = currentCup;
                int destinationValue = currentCup.Value;
                do
                {
                    destinationValue--;
                    if (destinationValue < minValue)
                    {
                        destinationValue = maxValue;
                    }
                }
                while (destinationValue == nextCup.Value || destinationValue == nextCup.Next.Value || destinationValue == nextCup.Next.Next.Value);
                Cup destinationCup = cups[destinationValue];
                Cup afterDestinationCup = destinationCup.Next;
                destinationCup.Next = nextCup;
                nextCup.Previous = destinationCup;
                afterDestinationCup.Previous = nextCup.Next.Next;
                nextCup.Next.Next.Next = afterDestinationCup;
                currentCup = currentCup.Next;
            }

            return cups[1];
        }

        // First attempt was fast enough for part one, but not part two.
        private static List<int> MoveCups(List<int> originalCups, int numberOfMoves)
        {
            List<int> cups = originalCups.ToList();
            int currentCup = cups.ElementAt(0);
            int cupCount = cups.Count();
            int cupMin = cups.Min();
            int cupMax = cups.Max();

            for (int moves = 1; moves <= numberOfMoves; moves++)
            {
                int currentCupIndex = cups.IndexOf(currentCup);
                List<int> pickupCups = new List<int>();
                for (int pickupCount = 1; pickupCount <= 3; pickupCount++)
                {
                    int pickupCup = cups[(currentCupIndex + pickupCount) % cupCount];
                    pickupCups.Add(pickupCup);
                }
                int destinationCup = currentCup - 1;
                foreach (int cup in pickupCups)
                {
                    cups.Remove(cup);
                }
                while (!cups.Contains(destinationCup))
                {
                    destinationCup--;
                    if (destinationCup < cupMin)
                    {
                        destinationCup = cupMax;
                    }
                }
                int destinationCupIndex = cups.IndexOf(destinationCup);
                cups.InsertRange(destinationCupIndex + 1, pickupCups);
                currentCup = cups[(cups.IndexOf(currentCup) + 1) % cupCount];
            }

            return cups;
        }
    }
}
