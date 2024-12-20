using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");
var matches = Regex.Matches(input, "mul\\((?<multiplicand>\\d{1,3}),(?<multiplier>\\d{1,3})\\)");
int productSum = 0;
foreach (Match match in matches)
{
    int multiplicand = int.Parse(match.Groups["multiplicand"].Value);
    int multiplier = int.Parse(match.Groups["multiplier"].Value);
    productSum += multiplicand * multiplier;
}
Console.WriteLine($"Sum of products: {productSum}");
