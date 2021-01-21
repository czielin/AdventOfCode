using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    public class Tile
    {
        public int Id { get; set; }
        public Tile Top { get; set; }
        public Tile Right { get; set; }
        public Tile Bottom { get; set; }
        public Tile Left { get; set; }
        public char[][] Pixels { get; set; } = new char[10][];
        public string TopRow
        {
            get
            {
                return new string(Pixels[0]);
            }
        }
        public string BottomRow
        {
            get
            {
                return new string(Pixels[9]);
            }
        }
        public string LeftColumn
        {
            get
            {
                var column = Pixels.Select(p => p[0]).ToArray();
                return new string(column);
            }
        }
        public string RightColumn
        {
            get
            {
                var column = Pixels.Select(p => p[9]).ToArray();
                return new string(column);
            }
        }
        public bool IsCorner
        {
            get
            {
                int nullCount = 0;
                nullCount += Left == null ? 0 : 1;
                nullCount += Right == null ? 0 : 1;
                nullCount += Top == null ? 0 : 1;
                nullCount += Bottom == null ? 0 : 1;
                return nullCount == 2;
            }
        }

        public void Rotate()
        {
            char[][] rotated = new char[10][];

            for (int x = 0; x < 10; x++)
            {
                rotated[x] = new char[10];
                for (int y = 0; y < 10; y++)
                {
                    rotated[x][y] = Pixels[9 - y][x];
                }
            }

            Pixels = rotated;
        }

        public void Rotate(Side currentOrientation, Side desiredOrientation)
        {
            int rotations = (desiredOrientation - currentOrientation) % 4;
            if (rotations < 0)
            {
                rotations = rotations + 4;
            }
            while (rotations > 0)
            {
                Rotate();
                rotations--;
            }
        }

        public void MirrorY()
        {
            char[][] mirrored = new char[10][];

            for (int x = 0; x < 10; x++)
            {
                mirrored[x] = new char[10];
                for (int y = 0; y < 10; y++)
                {
                    mirrored[x][y] = Pixels[9 - x][y];
                }
            }

            Pixels = mirrored;
        }

        public void MirrorX()
        {
            char[][] mirrored = new char[10][];

            for (int x = 0; x < 10; x++)
            {
                mirrored[x] = new char[10];
                for (int y = 0; y < 10; y++)
                {
                    mirrored[x][y] = Pixels[x][9 - y];
                }
            }

            Pixels = mirrored;
        }

        public void Print()
        {
            for (int x = 0; x < 10; x++)
            {
                Console.WriteLine(new string(Pixels[x]));
            }
        }

        public string SideString(Side side)
        {
            string sideString = null;

            switch (side)
            {
                case Side.Bottom:
                    sideString = BottomRow;
                    break;
                case Side.Left:
                    sideString = LeftColumn;
                    break;
                case Side.Top:
                    sideString = TopRow;
                    break;
                case Side.Right:
                    sideString = RightColumn;
                    break;
            }

            return sideString;
        }
    }
}
