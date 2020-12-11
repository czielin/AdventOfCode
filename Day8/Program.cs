using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day8
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            var instructions = lines.Select
            (
                l =>
                {
                    var instructionParts = l.Split(' ');
                    return (instructionParts[0], int.Parse(instructionParts[1]));
                }
            ).ToArray();
            int currentInstructionIndex = 0;
            int accumulator = 0;
            List<int> instuctionHistory = new List<int>();
            while (!instuctionHistory.Contains(currentInstructionIndex))
            {
                instuctionHistory.Add(currentInstructionIndex);
                var currentInstruction = instructions[currentInstructionIndex];
                switch (currentInstruction.Item1)
                {
                    case "acc":
                        accumulator += currentInstruction.Item2;
                        currentInstructionIndex++;
                        break;
                    case "jmp":
                        currentInstructionIndex = currentInstructionIndex + currentInstruction.Item2;
                        break;
                    case "nop":
                        currentInstructionIndex++;
                        break;
                }
            }

            Console.WriteLine($"The accumulator value before repeating is {accumulator}.");
        }
    }
}
