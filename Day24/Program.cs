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
        static async Task Main()
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
                // Hack to fill in neighoring tiles. Should be done cleaner.
                int blackNeighors = currentTile.BlackNeighbors;
            }

            Console.WriteLine($"Black tiles: { Tile.TileLocations.Values.Count(t => t.Color == Color.Black) }");

            for (int day = 1; day <= 100; day++)
            {
                List<Tile> tilesToFlip = new List<Tile>();
                List<Tile> existingTiles = Tile.TileLocations.Values.ToList();

                foreach (Tile tile in existingTiles)
                {
                    if (tile.Color == Color.Black && (tile.BlackNeighbors == 0 || tile.BlackNeighbors > 2))
                    {
                        tilesToFlip.Add(tile);
                    }
                    else if (tile.Color == Color.White && tile.BlackNeighbors == 2)
                    {
                        tilesToFlip.Add(tile);
                    }
                }

                foreach (Tile tile in tilesToFlip)
                {
                    tile.Flip();
                }

                Console.WriteLine($"Number of black tiles on Day {day} is: { Tile.TileLocations.Values.Count(t => t.Color == Color.Black) }");
            }
        }
    }
}
