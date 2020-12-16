using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await ProcessFile("sample_input.txt");
            await ProcessFile("sample_input2.txt");
            await ProcessFile("input.txt");
        }

        private static async Task ProcessFile(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);
            string mask = null;
            Dictionary<long, ulong> memory = new Dictionary<long, ulong>();

            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Replace("mask = ", "");
                }
                else
                {
                    var memParts = line.Split(new[] { "mem[", "] = " }, StringSplitOptions.RemoveEmptyEntries);
                    int memoryAddress = int.Parse(memParts[0]);
                    ulong memoryValue = ulong.Parse(memParts[1]);
                    ulong combinedValue = 0;
                    for (int position = 0; position < 36; position++)
                    {
                        if (mask[position] == '1')
                        {
                            combinedValue += (ulong)Math.Pow(2, 35 - position);
                        }
                        else if (mask[position] == 'X')
                        {
                            if (memoryValue >= (ulong)Math.Pow(2, 35 - position))
                            {
                                combinedValue += (ulong)Math.Pow(2, 35 - position);
                            }
                        }
                        if (memoryValue >= (ulong)Math.Pow(2, 35 - position))
                        {
                            memoryValue -= (ulong)Math.Pow(2, 35 - position);
                        }
                    }
                    memory[memoryAddress] = combinedValue;
                }
            }

            Console.WriteLine($"The sum of all values in memory is {memory.Values.Aggregate((i, j) => i + j)}");

            memory.Clear();
            int xCount = 0;

            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Replace("mask = ", "");
                    xCount = mask.Count(b => b == 'X');
                }
                else
                {
                    var memParts = line.Split(new[] { "mem[", "] = " }, StringSplitOptions.RemoveEmptyEntries);
                    int memoryAddress = int.Parse(memParts[0]);
                    string memoryAddressBinary = Convert.ToString(memoryAddress, 2).PadLeft(36, '0');
                    ulong memoryValue = ulong.Parse(memParts[1]);

                    for (int xIndex = 0; xIndex < Math.Pow(2, xCount); xIndex++)
                    {
                        int xPosition = 0;
                        string binary = Convert.ToString(xIndex, 2).PadLeft(xCount, '0');
                        char[] currentMemoryAddress = new char[36];

                        for (int position = 0; position < 36; position++)
                        {
                            if (mask[position] == '0')
                            {
                                currentMemoryAddress[position] = memoryAddressBinary[position];
                            }
                            if (mask[position] == '1')
                            {
                                currentMemoryAddress[position] = '1';
                            }
                            else if (mask[position] == 'X')
                            {
                                currentMemoryAddress[position] = binary[xPosition++];
                            }
                        }

                        memory[BinaryCharArrayToInt(currentMemoryAddress)] = memoryValue;
                    }
                }
            }

            foreach (var item in memory)
            {
                Console.WriteLine($"mem[{item.Key}] = {item.Value}");
            }

            Console.WriteLine($"The sum of all values in memory is {memory.Values.Aggregate((i, j) => i + j)}");
        }

        private static long BinaryCharArrayToInt(char[] charArray)
        {
            long intValue = 0;
            for (int position = 0; position < charArray.Length; position++)
            {
                if (charArray[position] == '1')
                {
                    intValue += (long)Math.Pow(2, 35 - position);
                }
            }
            return intValue;
        }
    }
}
