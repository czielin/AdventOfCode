using System;
using System.IO;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ProcessFile3d("sample_input.txt");
            await ProcessFile3d("input.txt");

            await ProcessFile4d("sample_input.txt");
            await ProcessFile4d("input.txt");
        }

        private static async Task ProcessFile4d(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);

            var grid = new char[lines.Length + 4, lines.Length + 4, 5, 5];

            for (var lineIndex = 2; lineIndex < lines.Length + 2; lineIndex++)
            {
                for (var charIndex = 2; charIndex < lines.Length + 2; charIndex++)
                {
                    grid[lineIndex, charIndex, 2, 2] = lines[lineIndex - 2][charIndex - 2];
                }
            }

            int totalActiveNeighbors = 0;

            for (int cycles = 0; cycles < 6; cycles++)
            {
                totalActiveNeighbors = 0;
                var newGrid = new char[grid.GetLength(0) + 2, grid.GetLength(1) + 2, grid.GetLength(2) + 2, grid.GetLength(3) + 2];
                for (int x = 1; x < grid.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < grid.GetLength(1) - 1; y++)
                    {
                        for (int z = 1; z < grid.GetLength(2) - 1; z++)
                        {
                            for (int w = 1; w < grid.GetLength(3) - 1; w++)
                            {
                                int activeNeighbors = 0;
                                for (int xOffset = -1; xOffset < 2; xOffset++)
                                {
                                    for (int yOffset = -1; yOffset < 2; yOffset++)
                                    {
                                        for (int zOffset = -1; zOffset < 2; zOffset++)
                                        {
                                            for (int wOffset = -1; wOffset < 2; wOffset++)
                                            {
                                                if (xOffset != 0 || yOffset != 0 || zOffset != 0 || wOffset != 0)
                                                {
                                                    activeNeighbors += grid[x + xOffset, y + yOffset, z + zOffset, w + wOffset] == '#' ? 1 : 0;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (grid[x, y, z, w] == '#')
                                {
                                    newGrid[x + 1, y + 1, z + 1, w + 1] = (activeNeighbors == 2 || activeNeighbors == 3) ? '#' : '.';
                                }
                                else
                                {
                                    newGrid[x + 1, y + 1, z + 1, w + 1] = activeNeighbors == 3 ? '#' : '.';
                                }
                                if (newGrid[x + 1, y + 1, z + 1, w + 1] == '#')
                                {
                                    totalActiveNeighbors++;
                                }
                            }
                        }
                    }
                }
                grid = newGrid;
            }

            Console.WriteLine($"Total number of active cubes: {totalActiveNeighbors}");
        }

        private static async Task ProcessFile3d(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);

            var grid = new char[lines.Length + 4, lines.Length + 4, 5];

            for (var lineIndex = 2; lineIndex < lines.Length + 2; lineIndex++)
            {
                for (var charIndex = 2; charIndex < lines.Length + 2; charIndex++)
                {
                    grid[lineIndex, charIndex, 2] = lines[lineIndex - 2][charIndex - 2];
                }
            }

            int totalActiveNeighbors = 0;

            for (int cycles = 0; cycles < 6; cycles++)
            {
                totalActiveNeighbors = 0;
                var newGrid = new char[grid.GetLength(0) + 2, grid.GetLength(1) + 2, grid.GetLength(2) + 2];
                for (int x = 1; x < grid.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < grid.GetLength(1) - 1; y++)
                    {
                        for (int z = 1; z < grid.GetLength(2) - 1; z++)
                        {
                            int activeNeighbors = 0;
                            activeNeighbors += grid[x - 1, y - 1, z - 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x - 1, y - 1, z] == '#' ? 1 : 0;
                            activeNeighbors += grid[x - 1, y - 1, z + 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x - 1, y, z - 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x - 1, y, z] == '#' ? 1 : 0;
                            activeNeighbors += grid[x - 1, y, z + 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x - 1, y + 1, z - 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x - 1, y + 1, z] == '#' ? 1 : 0;
                            activeNeighbors += grid[x - 1, y + 1, z + 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x, y - 1, z - 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x, y - 1, z] == '#' ? 1 : 0;
                            activeNeighbors += grid[x, y - 1, z + 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x, y, z - 1] == '#' ? 1 : 0;
                            //activeNeighbors += grid[x, y, z] == '#' ? 1 : 0;
                            activeNeighbors += grid[x, y, z + 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x, y + 1, z - 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x, y + 1, z] == '#' ? 1 : 0;
                            activeNeighbors += grid[x, y + 1, z + 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x + 1, y - 1, z - 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x + 1, y - 1, z] == '#' ? 1 : 0;
                            activeNeighbors += grid[x + 1, y - 1, z + 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x + 1, y, z - 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x + 1, y, z] == '#' ? 1 : 0;
                            activeNeighbors += grid[x + 1, y, z + 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x + 1, y + 1, z - 1] == '#' ? 1 : 0;
                            activeNeighbors += grid[x + 1, y + 1, z] == '#' ? 1 : 0;
                            activeNeighbors += grid[x + 1, y + 1, z + 1] == '#' ? 1 : 0;
                            if (grid[x, y, z] == '#')
                            {
                                newGrid[x + 1, y + 1, z + 1] = (activeNeighbors == 2 || activeNeighbors == 3) ? '#' : '.';
                            }
                            else
                            {
                                newGrid[x + 1, y + 1, z + 1] = activeNeighbors == 3 ? '#' : '.';
                            }
                            if (newGrid[x + 1, y + 1, z + 1] == '#')
                            {
                                totalActiveNeighbors++;
                            }
                        }
                    }
                }
                //Console.WriteLine($"Cycle: {cycles}");
                //PrintGrid(grid);
                grid = newGrid;
            }

            Console.WriteLine($"Total number of active cubes: {totalActiveNeighbors}");
        }

        private static void PrintGrid(char[,,] grid)
        {
            for (int z = 0; z < grid.GetLength(2); z++)
            {
                Console.WriteLine($"z = {z}");
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    for (int x = 0; x < grid.GetLength(0); x++)
                    {
                        Console.Write(grid[x, y, z]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
