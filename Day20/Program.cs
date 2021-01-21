using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day20
{
    class Program
    {
        private static List<Tile> tiles;
        private static HashSet<Tile> modifiedTiles = new HashSet<Tile>();

        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            Tile tile = null;
            int row = 0;
            tiles = new List<Tile>();
            foreach (string line in lines)
            {
                if (line.StartsWith("Tile"))
                {
                    tile = new Tile();
                    tile.Id = int.Parse(line.Replace("Tile ", "").Replace(":", ""));
                    tiles.Add(tile);
                    row = 0;
                }
                else if (line != "")
                {
                    tile.Pixels[row] = line.ToCharArray();
                    row++;
                }
            }

            foreach (Tile currentTile in tiles)
            {
                modifiedTiles.Add(currentTile);
            }

            while (modifiedTiles.Any())
            {
                Tile currentTile = modifiedTiles.First();
                currentTile.Top = FindMatch(currentTile, Side.Top);
                currentTile.Right = FindMatch(currentTile, Side.Right);
                currentTile.Bottom = FindMatch(currentTile, Side.Bottom);
                currentTile.Left = FindMatch(currentTile, Side.Left);
                modifiedTiles.Remove(currentTile);
            }

            var corners = tiles.Where(t => t.IsCorner);
            long product = 1;
            foreach (Tile corner in corners)
            {
                product = product * corner.Id;
            }

            Console.WriteLine($"Corner product is: { product }");

            Tile topLeft = corners.Single(c => c.Top == null && c.Left == null);

            int dimension = 0;
            Tile rowTile = topLeft;

            while (rowTile != null)
            {
                dimension++;
                rowTile = rowTile.Bottom;
            }

            Tile[,] tileGrid = new Tile[dimension, dimension];

            rowTile = topLeft;
            Tile columnTile = topLeft;
            row = 0;
            int column;

            while (rowTile != null)
            {
                column = 0;
                while (columnTile != null)
                {
                    tileGrid[row, column] = columnTile;
                    columnTile = columnTile.Right;
                    column++;
                }
                columnTile = rowTile.Bottom;
                rowTile = rowTile.Bottom;
                row++;
            }

            // Consolidate tiles and get rid of border.
            char[,] image = new char[dimension * 8, dimension * 8];

            row = 0;
            column = 0;
            int hashes = 0;

            for (int tileColumn = 0; tileColumn < dimension; tileColumn++)
            {
                for (int tileRow = 0; tileRow < dimension; tileRow++)
                {
                    tile = tileGrid[tileRow, tileColumn];
                    for (row = 1; row < 9; row++)
                    {
                        for (column = 1; column < 9; column++)
                        {
                            image[(row - 1) + (tileRow * 8), (column - 1) + (tileColumn * 8)] = tile.Pixels[row][column];
                            if (tile.Pixels[row][column] == '#')
                            {
                                hashes++;
                            }
                        }
                    }
                }
            }

            // Find sea dragons.
            int seaDragons = 0;
            int rotations = 0;
            int mirrors = 0;
            do
            {
                do
                {
                    for (row = 1; row < image.GetLength(0) - 1; row++)
                    {
                        for (column = 0; column < image.GetLength(1) - 19; column++)
                        {
                            if
                            (
                                image[row, column] == '#'
                                && image[row + 1, column + 1] == '#'
                                && image[row + 1, column + 4] == '#'
                                && image[row, column + 5] == '#'
                                && image[row, column + 6] == '#'
                                && image[row + 1, column + 7] == '#'
                                && image[row + 1, column + 10] == '#'
                                && image[row, column + 11] == '#'
                                && image[row, column + 12] == '#'
                                && image[row + 1, column + 13] == '#'
                                && image[row + 1, column + 16] == '#'
                                && image[row, column + 17] == '#'
                                && image[row, column + 18] == '#'
                                && image[row - 1, column + 18] == '#'
                                && image[row, column + 19] == '#'
                            )
                            {
                                seaDragons++;
                            }
                        }
                    }
                    Console.WriteLine($"Hashes that aren't a part of a sea dragon: { hashes - (seaDragons * 15) }");
                    //Print(image);
                    image = Rotate(image);
                    rotations++;
                    seaDragons = 0;
                }
                while (rotations < 4);
                image = Mirror(image);
                mirrors++;
            }
            while (mirrors < 2);
        }

        private static void Print(char[,] image)
        {
            for (int row = 0; row < image.GetLength(0); row++)
            {
                for (int column = 0; column < image.GetLength(1); column++)
                {
                    Console.Write(image[row, column]);
                }
                Console.WriteLine();
            }
        }

        private static char[,] Mirror(char[,] image)
        {
            char[,] mirrored = new char[image.GetLength(0), image.GetLength(1)];

            for (int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    mirrored[x, y] = image[image.GetLength(0) - 1 - x, y];
                }
            }

            return mirrored;
        }

        private static char[,] Rotate(char[,] grid)
        {
            char[,] rotated = new char[grid.GetLength(0), grid.GetLength(1)];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    rotated[x, y] = grid[grid.GetLength(0) - 1 - y, x];
                }
            }

            return rotated;
        }

        private static Side Opposite(Side side)
        {
            Side opposite;

            switch (side)
            {
                case Side.Top:
                    opposite = Side.Bottom;
                    break;
                case Side.Right:
                    opposite = Side.Left;
                    break;
                case Side.Bottom:
                    opposite = Side.Top;
                    break;
                default:
                    opposite = Side.Right;
                    break;
            }

            return opposite;
        }

        private static Tile FindMatch(Tile tile, Side side)
        {
            string sideString = tile.SideString(side);
            char[] charsReversed = sideString.ToCharArray();
            Array.Reverse(charsReversed);
            string sideReversed = new string(charsReversed);
            Tile match = null;
            Side oppositeSide = Opposite(side);
            foreach (Tile tileToCheck in tiles)
            {
                if (tile.Id != tileToCheck.Id)
                {
                    string initialTopRow = tileToCheck.TopRow;
                    if (tileToCheck.TopRow == sideString || tileToCheck.TopRow == sideReversed)
                    {
                        tileToCheck.Rotate(Side.Top, oppositeSide);
                        match = tileToCheck;
                    }
                    else if (tileToCheck.RightColumn == sideString || tileToCheck.RightColumn == sideReversed)
                    {
                        tileToCheck.Rotate(Side.Right, oppositeSide);
                        match = tileToCheck;
                    }
                    else if (tileToCheck.BottomRow == sideString || tileToCheck.BottomRow == sideReversed)
                    {
                        tileToCheck.Rotate(Side.Bottom, oppositeSide);
                        match = tileToCheck;
                    }
                    else if (tileToCheck.LeftColumn == sideString || tileToCheck.LeftColumn == sideReversed)
                    {
                        tileToCheck.Rotate(Side.Left, oppositeSide);
                        match = tileToCheck;
                    }
                    if (tileToCheck.TopRow == sideReversed || tileToCheck.BottomRow == sideReversed)
                    {
                        tileToCheck.MirrorX();
                    }
                    else if (tileToCheck.LeftColumn == sideReversed || tileToCheck.RightColumn == sideReversed)
                    {
                        tileToCheck.MirrorY();
                    }
                    if (initialTopRow != tileToCheck.TopRow)
                    {
                        modifiedTiles.Add(tileToCheck);
                    }
                }
            }
            return match;
        }
    }
}
