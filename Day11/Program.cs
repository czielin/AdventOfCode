using System;
using System.IO;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            int width = lines[0].Length;
            int height = lines.Length;
            char[,] initialLayout = new char[lines.Length, lines[0].Length];
            for (int line = 0; line < height; line++)
            {
                for (int column = 0; column < width; column++)
                {
                    initialLayout[line, column] = lines[line][column];
                }
            }

            FindStableLayout(initialLayout, width, height, 4, GetFilledNeighbors);
            FindStableLayout(initialLayout, width, height, 5, GetVisibleNeighbors);
        }

        private static void FindStableLayout(char[,] initialLayout, int width, int height, int neighborsTolerated, Func<int, int, int, int, char[,], int> neighborCount)
        {
            char[,] existingLayout = initialLayout;

            int changedSeats;
            int filledSeats;
            int iterations = 0;

            do
            {
                changedSeats = 0;
                filledSeats = 0;
                iterations++;
                var newLayout = new char[height, width];
                for (int row = 0; row < height; row++)
                {
                    for (int column = 0; column < width; column++)
                    {
                        int filledNeighbors = neighborCount(row, column, height, width, existingLayout);
                        if (existingLayout[row, column] == 'L' && filledNeighbors == 0)
                        {
                            newLayout[row, column] = '#';
                            changedSeats++;
                            filledSeats++;
                        }
                        else if (existingLayout[row, column] == '#' && filledNeighbors >= neighborsTolerated)
                        {
                            newLayout[row, column] = 'L';
                            changedSeats++;
                        }
                        else
                        {
                            newLayout[row, column] = existingLayout[row, column];
                            if (existingLayout[row, column] == '#')
                            {
                                filledSeats++;
                            }
                        }
                    }
                }
                existingLayout = newLayout;
            }
            while (changedSeats > 0);

            Console.WriteLine($"There are {filledSeats} filled seats.");
            Console.WriteLine($"Executed {iterations} iterations.");
        }

        private static bool IsSeatFiled(int row, int column, int totalRows, int totalColumns, char[,] layout)
        {
            return row >= 0 && row < totalRows && column >= 0 && column < totalColumns && layout[row, column] == '#';
        }

        private static int GetFilledNeighbors(int row, int column, int totalRows, int totalColumns, char[,] layout)
        {
            int filledNeighbors = 0;
            filledNeighbors += IsSeatFiled(row - 1, column - 1, totalRows, totalColumns, layout) ? 1 : 0;
            filledNeighbors += IsSeatFiled(row - 1, column, totalRows, totalColumns, layout) ? 1 : 0;
            filledNeighbors += IsSeatFiled(row - 1, column + 1, totalRows, totalColumns, layout) ? 1 : 0;
            filledNeighbors += IsSeatFiled(row, column - 1, totalRows, totalColumns, layout) ? 1 : 0;
            filledNeighbors += IsSeatFiled(row, column + 1, totalRows, totalColumns, layout) ? 1 : 0;
            filledNeighbors += IsSeatFiled(row + 1, column - 1, totalRows, totalColumns, layout) ? 1 : 0;
            filledNeighbors += IsSeatFiled(row + 1, column, totalRows, totalColumns, layout) ? 1 : 0;
            filledNeighbors += IsSeatFiled(row + 1, column + 1, totalRows, totalColumns, layout) ? 1 : 0;
            return filledNeighbors;
        }

        private static int GetVisibleNeighbors(int row, int column, int totalRows, int totalColumns, char[,] layout)
        {
            int visibleNeighbors = 0;
            visibleNeighbors += IsNeighborVisible(row, column, -1, -1, totalRows, totalColumns, layout) ? 1 : 0;
            visibleNeighbors += IsNeighborVisible(row, column, -1, 0, totalRows, totalColumns, layout) ? 1 : 0;
            visibleNeighbors += IsNeighborVisible(row, column, -1, 1, totalRows, totalColumns, layout) ? 1 : 0;
            visibleNeighbors += IsNeighborVisible(row, column, 0, -1, totalRows, totalColumns, layout) ? 1 : 0;
            visibleNeighbors += IsNeighborVisible(row, column, 0, 1, totalRows, totalColumns, layout) ? 1 : 0;
            visibleNeighbors += IsNeighborVisible(row, column, 1, -1, totalRows, totalColumns, layout) ? 1 : 0;
            visibleNeighbors += IsNeighborVisible(row, column, 1, 0, totalRows, totalColumns, layout) ? 1 : 0;
            visibleNeighbors += IsNeighborVisible(row, column, 1, 1, totalRows, totalColumns, layout) ? 1 : 0;
            return visibleNeighbors;
        }

        private static bool IsNeighborVisible(int row, int column, int rowStep, int columnStep, int totalRows, int totalColumns, char[,] layout)
        {
            int newRow = row + rowStep;
            int newColumn = column + columnStep;
            bool isVisible = false;
            if (newRow >= 0 && newRow < totalRows && newColumn >= 0 && newColumn < totalColumns)
            {
                char seatValue = layout[newRow, newColumn];
                if (seatValue == '#')
                {
                    isVisible = true;
                }
                else if (seatValue == '.')
                {
                    isVisible = IsNeighborVisible(newRow, newColumn, rowStep, columnStep, totalRows, totalColumns, layout);
                }
            }
            return isVisible;
        }
    }
}
