var lines = File.ReadAllLines("input.txt");
long validLinesSum = 0;
long validLinesThreeOperatorsSum = 0;

foreach (var line in lines)
{
    var lineParts  = line.Split(' ');
    long answer = long.Parse(lineParts[0].Trim(':'));
    var digits = lineParts[1..].Select(d => int.Parse(d)).ToList();

    if (IsEquationValid(answer, digits, 2))
    {
        validLinesSum += answer;
    }

    if (IsEquationValid(answer, digits, 3))
    {
        validLinesThreeOperatorsSum += answer;
    }
}

Console.WriteLine($"The sum of valid lines is: {validLinesSum}");
Console.WriteLine($"The sum of valid lines with three operators is: {validLinesThreeOperatorsSum}");
Console.ReadLine();

bool IsEquationValid(long answer, List<int> digits, int numberOfOperators)
{
    bool isValid = false;
    for (int operatorCounter = 0; operatorCounter < Math.Pow(numberOfOperators, digits.Count - 1); operatorCounter++)
    {
        long result = digits[0];
        long amountUsed = 0;
        for (int digitCount = digits.Count - 1; digitCount > 0; digitCount--)
        {
            long remainingAmount = operatorCounter - amountUsed;
            int operatorType = (int)Math.Floor((double)remainingAmount / Math.Pow(numberOfOperators, digitCount - 1));
            amountUsed += operatorType * (long)Math.Pow(numberOfOperators, digitCount - 1);
            int nextDigitIndex = digits.Count - digitCount;
            if (operatorType == 0)
            {
                result += digits[nextDigitIndex];
            }
            else if (operatorType == 1)
            {
                result *= digits[nextDigitIndex];
            }
            else
            {
                string concat = result.ToString() + digits[nextDigitIndex];
                result = long.Parse(concat);
            }
        }
        if (result == answer)
        {
            isValid = true;
            break;
        }
    }
    return isValid;
}