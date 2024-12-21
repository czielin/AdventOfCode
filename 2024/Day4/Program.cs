var lines = File.ReadAllLines("input.txt");
int xmasCount = 0;
int columnCount = lines[0].Length;
int rowCount = lines.Length;

for (int row  = 0; row < rowCount; row++)
{
    for (int column = 0; column < columnCount; column++)
    {
        xmasCount += spellsXmas(row, column, 1, 1) ? 1 : 0;
        xmasCount += spellsXmas(row, column, 1, 0) ? 1 : 0;
        xmasCount += spellsXmas(row, column, 1, -1) ? 1 : 0;
        xmasCount += spellsXmas(row, column, 0, -1) ? 1 : 0;
        xmasCount += spellsXmas(row, column, -1, 1) ? 1 : 0;
        xmasCount += spellsXmas(row, column, -1, 0) ? 1 : 0;
        xmasCount += spellsXmas(row, column, -1, -1) ? 1 : 0;
        xmasCount += spellsXmas(row, column, 0, 1) ? 1 : 0;
    }
}

Console.WriteLine($"XMASs found: {xmasCount}");
Console.ReadLine();

bool spellsXmas(int row, int column, int rowStep, int columnStep)
{
    bool isXmas = false;
    int endRow = row + (rowStep * 3);
    int endColumn = column + (columnStep * 3);
    // Check boundaries
    if (endRow < rowCount && endRow >= 0 && endColumn < columnCount && endColumn >= 0)
    {
        isXmas = lines[row][column] == 'X'
            && lines[(row + rowStep) % rowCount][(column + columnStep) % columnCount] == 'M'
            && lines[(row + rowStep * 2) % rowCount][(column + columnStep * 2) % columnCount] == 'A'
            && lines[(row + rowStep * 3) % rowCount][(column + columnStep * 3) % columnCount] == 'S';
    }
    return isXmas;
}

