namespace Day6
{
    internal record Position
    {
        public static readonly Position Up = new Position { Row = -1, Column = 0 };
        public static readonly Position Right = new Position { Row = 0, Column = 1 };
        public static readonly Position Down = new Position { Row = 1, Column = 0 };
        public static readonly Position Left = new Position { Row = 0, Column = -1 };

        public int Row { get; set; }
        public int Column { get; set; }

        public Position Move(Position direction)
        {
            return new Position
            {
                Row = this.Row + direction.Row,
                Column = this.Column + direction.Column
            };
        }
    }
}
