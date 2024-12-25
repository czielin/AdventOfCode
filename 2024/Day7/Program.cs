var lines = File.ReadAllLines("input.txt");
long validLinesSum = 0;

foreach (var line in lines)
{
    var lineParts  = line.Split(' ');
    long answer = long.Parse(lineParts[0].Trim(':'));
    var digits = lineParts[1..].Select(d => int.Parse(d)).ToList();
    for (int operatorCounter = 0; operatorCounter < Math.Pow(2, digits.Count) - 1; operatorCounter++)
    {
        long result = digits[0];
        for (int digitCount = 1; digitCount < digits.Count; digitCount++)
        {
            int operatorType = operatorCounter & (int)Math.Pow(2, digitCount - 1);
            if (operatorType == 0)
            {
                result += digits[digitCount];
            }
            else
            {
                result *= digits[digitCount];
            }
        }
        if (result == answer)
        {
            validLinesSum += answer;
            break;
        }
    }
}

Console.WriteLine($"The sum of valid lines is: {validLinesSum}");
Console.ReadLine();