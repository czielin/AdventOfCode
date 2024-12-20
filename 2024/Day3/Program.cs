using System.Text.RegularExpressions;

string regex = "(mul\\((?<multiplicand>\\d{1,3}),(?<multiplier>\\d{1,3})\\))|do\\(\\)|don't\\(\\)";
var input = File.ReadAllText("input.txt");
var matches = Regex.Matches(input, regex);
int productSum = 0;
int conditionalProductSum = 0;
bool enabled = true;
foreach (Match match in matches)
{
    if (match.Value == "do()")
    {
        enabled = true;
    }
    else if (match.Value == "don't()")
    {
        enabled = false;
    }
    else
    {
        int multiplicand = int.Parse(match.Groups["multiplicand"].Value);
        int multiplier = int.Parse(match.Groups["multiplier"].Value);
        productSum += multiplicand * multiplier;
        if (enabled)
        {
            conditionalProductSum += multiplicand * multiplier;
        }
    }
    
}
Console.WriteLine($"Sum of products: {productSum}");
Console.WriteLine($"Conditional sum of products: {conditionalProductSum}");
Console.ReadLine();