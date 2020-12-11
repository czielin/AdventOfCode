using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HashSet<int> seatIds = new HashSet<int>();
            var lines = await File.ReadAllLinesAsync("input.txt");
            int maxId = 0;
            foreach (string line in lines)
            {
                int seatId = GetSeatId(line);
                seatIds.Add(seatId);
                if (seatId > maxId)
                {
                    maxId = seatId;
                }
            }
            for (int count = 0; count < maxId; count++)
            {
                if (seatIds.Contains(count) && !seatIds.Contains(count + 1))
                {
                    Console.WriteLine($"My seat ID is: {count + 1}");
                }
            }
            Console.WriteLine($"The highest seat ID is: {maxId}");
        }

        private static int GetSeatId(string partitionString)
        {
            var seat = GetSeat(partitionString);
            return seat.row * 8 + seat.column;
        }

        private static (int row, int column) GetSeat(string partitionString)
        {
            int row = 0;
            for (int count = 0; count < 7; count++)
            {
                if (partitionString[count] == 'B')
                {
                    row += (int)Math.Pow(2, (6 - count));
                }
            }
            int column = 0;
            string columnString = partitionString.Substring(7, 3);
            for (int count = 0; count < 3; count++)
            {
                if (columnString[count] == 'R')
                {
                    column += (int)Math.Pow(2, (2 - count));
                }
            }
            return (row, column);
        }
    }
}
