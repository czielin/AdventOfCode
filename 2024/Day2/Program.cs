using System.Runtime.CompilerServices;

bool isReportSafe(IEnumerable<int> levels)
{
    bool increasing = levels.ElementAt(0) < levels.ElementAt(1);
    bool isSafe = true;
    for (int i = 0; i < levels.Count() - 1; i++)
    {
        int difference = levels.ElementAt(i + 1) - levels.ElementAt(i);
        int absoluteDiffence = Math.Abs(difference);

        if
        (
            (increasing && difference > 0 || !increasing && difference < 0)
            && absoluteDiffence >= 1
            && absoluteDiffence <= 3
        )
        {
            // safe.
        }
        else
        {
            isSafe = false;
            break;
        }
    }
    return isSafe;
}

bool isDampenedReportSafe(List<int> levels)
{
    bool isSafe = false;
    for (int i = 0; i < levels.Count && !isSafe; i++)
    {
        var levelsCopy = new List<int>(levels);
        levelsCopy.RemoveAt(i);
        isSafe = isReportSafe(levelsCopy);
    }
    return isSafe;
}

var lines = File.ReadAllLines("input.txt");
int safeCount = 0;
int dampenedSafeCount = 0;
foreach (var line in lines)
{
    var levels = line.Split(' ').Select(l => int.Parse(l)).ToList();

    if (isReportSafe(levels))
    {
        safeCount++;
        dampenedSafeCount++;
    }
    else if (isDampenedReportSafe(levels))
    {
        dampenedSafeCount++;
    }
}
Console.WriteLine($"Safe count: {safeCount}");
Console.WriteLine($"Dampened safe count: {dampenedSafeCount}");
Console.ReadLine();
