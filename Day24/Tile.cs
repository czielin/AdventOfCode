using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Day24
{
    public class Tile
    {
        private readonly int x;
        private readonly int y;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            TileLocations.Add((x, y), this);
        }

        public static Dictionary<(int x, int y), Tile> TileLocations = new Dictionary<(int x, int y), Tile>();
        public Color Color { get; set; } = Color.White;
        public Tile East
        {
            get
            {
                if (!TileLocations.ContainsKey((x + 2, y)))
                {
                    new Tile(x + 2, y);
                }
                return TileLocations[(x + 2, y)];
            }
        }
        public Tile SouthEast
        {
            get
            {
                if (!TileLocations.ContainsKey((x + 1, y - 1)))
                {
                    new Tile(x + 1, y - 1);
                }
                return TileLocations[(x + 1, y - 1)];
            }
        }
        public Tile SouthWest
        {
            get
            {
                if (!TileLocations.ContainsKey((x - 1, y - 1)))
                {
                    new Tile(x - 1, y - 1);
                }
                return TileLocations[(x - 1, y - 1)];
            }
        }
        public Tile West
        {
            get
            {
                if (!TileLocations.ContainsKey((x - 2, y)))
                {
                    new Tile(x - 2, y);
                }
                return TileLocations[(x - 2, y)];
            }
        }
        public Tile NorthWest
        {
            get
            {
                if (!TileLocations.ContainsKey((x - 1, y + 1)))
                {
                    new Tile(x - 1, y + 1);
                }
                return TileLocations[(x - 1, y + 1)];
            }
        }
        public Tile NorthEast
        {
            get
            {
                if (!TileLocations.ContainsKey((x + 1, y + 1)))
                {
                    new Tile(x + 1, y + 1);
                }
                return TileLocations[(x + 1, y + 1)];
            }
        }
        public int BlackNeighbors
        {
            get
            {
                return (East.Color == Color.Black ? 1 : 0)
                    + (SouthEast.Color == Color.Black ? 1 : 0)
                    + (SouthWest.Color == Color.Black ? 1 : 0)
                    + (West.Color == Color.Black ? 1 : 0)
                    + (NorthWest.Color == Color.Black ? 1 : 0)
                    + (NorthEast.Color == Color.Black ? 1 : 0);
            }
        }
        public void Flip()
        {
            Color = Color == Color.White ? Color.Black : Color.White;
        }
    }
}
