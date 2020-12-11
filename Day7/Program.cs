using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            Dictionary<string, Bag> bags = new Dictionary<string, Bag>();

            foreach (string line in lines)
            {
                var bagParts = line.TrimEnd('.').Split("s contain ");
                string parentBagName = bagParts[0];
                if (!bags.ContainsKey(parentBagName))
                {
                    var bagNameParts = parentBagName.Split(" ");
                    bags.Add(parentBagName, new Bag { Name = parentBagName, Style = bagNameParts[0], Color = bagNameParts[1] });
                }
                Bag parentBag = bags[parentBagName];
                if (bagParts[1] != "no other bags")
                {
                    var childBagsParts = bagParts[1].Split(", ");
                    foreach (string childBagString in childBagsParts)
                    {
                        var childBagParts = childBagString.Split(' ', 2);
                        int numberOfChildren = int.Parse(childBagParts[0]);
                        string childBagName = childBagParts[1].TrimEnd('s');
                        if (!bags.ContainsKey(childBagName))
                        {
                            var bagNameParts = childBagName.Split(" ");
                            bags.Add(childBagName, new Bag { Name = childBagName, Style = bagNameParts[0], Color = bagNameParts[1] });
                        }
                        Bag childBag = bags[childBagName];
                        parentBag.CanContainBags.Add((numberOfChildren, childBag));
                        childBag.CanBeContainedBy.Add((numberOfChildren, parentBag));
                    }
                }
            }

            Queue<string> bagsToProcess = new Queue<string>();
            bagsToProcess.Enqueue("shiny gold bag");
            HashSet<string> parentBags = new HashSet<string>();

            while (bagsToProcess.Any())
            {
                string currentBagName = bagsToProcess.Dequeue();
                Bag currentBag = bags[currentBagName];
                foreach (var parentBagMap in currentBag.CanBeContainedBy)
                {
                    Bag parentBag = parentBagMap.Item2;
                    if (!parentBags.Contains(parentBag.Name))
                    {
                        bagsToProcess.Enqueue(parentBag.Name);
                        parentBags.Add(parentBag.Name);
                    }
                }
            }

            Console.WriteLine($"Found {parentBags.Count} bags that can contain a shiny gold bag.");
            Console.WriteLine($"These bags have {parentBags.Select(n => bags[n].Color).Distinct().Count()} distinct colors.");

            Console.WriteLine($"Found {bags["shiny gold bag"].GetNumberOfChildBags()} bags that can be contained by a shiny gold bag.");
        }
    }
}
