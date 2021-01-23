using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day24
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            Tile referenceTile = new Tile(0, 0);

            foreach (string line in lines)
            {
                Tile currentTile = referenceTile;
                for (int index = 0; index < line.Length; index++)
                {
                    char direction = line[index];
                    if (direction == 'n')
                    {
                        index++;
                        char subDirection = line[index];
                        if (subDirection == 'e')
                        {
                            currentTile = currentTile.NorthEast;
                        }
                        else
                        {
                            currentTile = currentTile.NorthWest;
                        }
                    }
                    else if (direction == 's')
                    {
                        index++;
                        char subDirection = line[index];
                        if (subDirection == 'e')
                        {
                            currentTile = currentTile.SouthEast;
                        }
                        else
                        {
                            currentTile = currentTile.SouthWest;
                        }
                    }
                    else if (direction == 'e')
                    {
                        currentTile = currentTile.East;
                    }
                    else
                    {
                        currentTile = currentTile.West;
                    }
                }

                currentTile.Flip();
            }

            Console.WriteLine($"Black tiles: { Tile.TileLocations.Values.Count(t => t.Color == Color.Black) }");
        }
    }
}
