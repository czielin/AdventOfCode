using System.Text;

var lines = File.ReadAllLines("input.txt");
int xmasCount = 0;
int xmasShapeCount = 0;
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
        xmasShapeCount += isXmas(row, column) ? 1 : 0;
    }
}

Console.WriteLine($"XMASs found: {xmasCount}");
Console.WriteLine($"XMAS shapes found: {xmasShapeCount}");
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

bool isXmas(int row, int column)
{
    bool isXmas = false;
    // Check boundaries
    if (lines[row][column] == 'A' && row < rowCount - 1 && row > 0 && column < columnCount - 1 && column > 0)
    {
        StringBuilder cornersBuilder = new StringBuilder(4);
        cornersBuilder.Append(lines[row - 1][column - 1]);
        cornersBuilder.Append(lines[row - 1][column + 1]);
        cornersBuilder.Append(lines[row + 1][column + 1]);
        cornersBuilder.Append(lines[row + 1][column - 1]);
        string corners = cornersBuilder.ToString();
        isXmas = corners == "MMSS" || corners == "SMMS" || corners == "SSMM" || corners == "MSSM";
    }
    return isXmas;
}
