using Day6;
using System.Data;

char[][] lines = null;
Position startPosition = null;
Position currentPosition = null;
Position direction = null;
ResetMap();
int width = lines[0].Length;
int height = lines.Length;

int positionsVisited = 1;
int loops = 0;

for (int row = 0; row < lines.Length; row++)
{
    for (int column = 0; column < width; column++)
    {
        if (lines[row][column] == '^')
        {
            startPosition = currentPosition = new Position { Row = row, Column = column };
        }
    }
}

WalkTheMap();

Console.WriteLine($"Number of unique positions visited: {positionsVisited}");

for (int row = 0; row < lines.Length; row++)
{
    for (int column = 0; column < width; column++)
    {
        ResetMap();
        if (lines[row][column] == '.')
        {
            lines[row][column] = '#';
            bool isALoop = WalkTheMap();
            if (isALoop)
            {
                loops++;
            }
        }
    }
}

Console.WriteLine($"Number of looping maps: {loops}");
Console.ReadLine();

bool WalkTheMap()
{
    bool isALoop = false;
    Position nextPosition = currentPosition.Move(direction);

    while (IsInBounds(nextPosition))
    {
        var character = lines[nextPosition.Row][nextPosition.Column];
        char directionChar = GetDirectionChar();
        if (character == '#')
        {
            TurnRight();
        }
        else if (directionChar == character)
        {
            isALoop = true;
            break;
        }
        else
        {
            if (character == '.')
            {
                positionsVisited++;
                lines[nextPosition.Row][nextPosition.Column] = directionChar;
            }
            currentPosition = nextPosition;
        }
        nextPosition = currentPosition.Move(direction);
    }

    return isALoop;
}

char GetDirectionChar()
{
    char directionChar;
    if (direction == Position.Up)
    {
        directionChar = 'U';
    }
    else if (direction == Position.Right)
    {
        directionChar = 'R';
    }
    else if (direction == Position.Down)
    {
        directionChar = 'D';
    }
    else
    {
        directionChar = 'L';
    }
    return directionChar;
}

void ResetMap()
{
    positionsVisited = 1;
    lines = File.ReadAllLines("input.txt").Select(s => s.ToCharArray()).ToArray();
    currentPosition = startPosition;
    direction = Position.Up;
}

void PrintGrid()
{
    foreach (var line in lines)
    {
        foreach (var character in line)
        {
            Console.Write(character);
        }
        Console.WriteLine();
    }
}

void TurnRight()
{
    if (direction == Position.Up)
    {
        direction = Position.Right;
    }
    else if (direction == Position.Right)
    {
        direction = Position.Down;
    }
    else if (direction == Position.Down)
    {
        direction = Position.Left;
    }
    else if (direction == Position.Left)
    {
        direction = Position.Up;
    }
}

bool IsInBounds(Position position)
{
    return position.Row >= 0
        && position.Row < height
        && position.Column >= 0
        && position.Column < width;
}