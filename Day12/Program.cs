using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        private static int x = 0;
        private static int y = 0;
        private static int currentHeading = 90;
        private static int wayPointX = 10;
        private static int wayPointY = 1;

        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            var directions = lines.Select
            (
                l =>
                {
                    char direction = l[0];
                    int value = int.Parse(l.Substring(1));
                    return (direction, value);
                }
            );

            foreach (var direction in directions)
            {
                Move(direction.direction, direction.value);
            }

            int manhattanDistance = Math.Abs(x) + Math.Abs(y);
            Console.WriteLine($"The manhattan distance from the start point is {manhattanDistance}.");

            x = 0;
            y = 0;

            foreach (var direction in directions)
            {
                MoveWayPoint(direction.direction, direction.value);
            }

            manhattanDistance = Math.Abs(x) + Math.Abs(y);
            Console.WriteLine($"The manhattan distance from the start point is {manhattanDistance}.");
        }

        private static void MoveWayPoint(char direction, int value)
        {
            switch (direction)
            {
                case 'N':
                    wayPointY += value;
                    break;
                case 'S':
                    wayPointY -= value;
                    break;
                case 'E':
                    wayPointX += value;
                    break;
                case 'W':
                    wayPointX -= value;
                    break;
                case 'L':
                    RotateWayPoint(value * -1);
                    break;
                case 'R':
                    RotateWayPoint(value);
                    break;
                case 'F':
                    Move('N', value * wayPointY);
                    Move('E', value * wayPointX);
                    break;
            }
        }

        private static void RotateWayPoint(int degrees)
        {
            if (degrees > 0)
            {
                int tempX = wayPointY;
                wayPointY = wayPointX * -1;
                wayPointX = tempX;
                RotateWayPoint(degrees - 90);
            }
            else if (degrees < 0)
            {
                int tempX = wayPointY * -1;
                wayPointY = wayPointX;
                wayPointX = tempX;
                RotateWayPoint(degrees + 90);
            }
        }

        private static void Move(char direction, int value)
        {
            switch (direction)
            {
                case 'N':
                    y += value;
                    break;
                case 'S':
                    y -= value;
                    break;
                case 'E':
                    x += value;
                    break;
                case 'W':
                    x -= value;
                    break;
                case 'L':
                    currentHeading -= value;
                    break;
                case 'R':
                    currentHeading += value;
                    break;
                case 'F':
                    Move(GetCurrentDirection(), value);
                    break;
            }
        }

        private static char GetCurrentDirection()
        {
            char direction;
            int heading = currentHeading % 360;
            if (heading < 0)
            {
                heading += 360;
            }
            switch (heading % 360)
            {
                case 0:
                    direction = 'N';
                    break;
                case 90:
                    direction = 'E';
                    break;
                case 180:
                    direction = 'S';
                    break;
                case 270:
                    direction = 'W';
                    break;
                default:
                    throw new Exception("Unexpected direction.");
            }
            return direction;
        }
    }
}
