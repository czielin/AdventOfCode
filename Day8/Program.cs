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

            var part1Result = ExecuteInstructions(instructions);

            Console.WriteLine($"The accumulator value before repeating is {part1Result.Accumulator}.");

            for (int index = 0; index < instructions.Length; index++)
            {
                var currentInstruction = instructions[index];
                if (currentInstruction.Item1 == "jmp")
                {
                    instructions[index] = ("nop", currentInstruction.Item2);
                }
                else if (currentInstruction.Item1 == "nop")
                {
                    instructions[index] = ("jmp", currentInstruction.Item2);
                }

                if (currentInstruction.Item1 != "acc")
                {
                    var result = ExecuteInstructions(instructions);
                    instructions[index] = currentInstruction;
                    if (result.ReachedEnd)
                    {
                        Console.WriteLine($"Accumulator value after successful execution is {result.Accumulator}.");
                        break;
                    }
                }
            }
        }

        private static (bool ReachedEnd, int Accumulator) ExecuteInstructions((string, int)[] instructions)
        {
            int currentInstructionIndex = 0;
            int accumulator = 0;
            List<int> instuctionHistory = new List<int>();
            while (!instuctionHistory.Contains(currentInstructionIndex) && currentInstructionIndex != instructions.Length)
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
            return (currentInstructionIndex == instructions.Length, accumulator);
        }
    }
}
