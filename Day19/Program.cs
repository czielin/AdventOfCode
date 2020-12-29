using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            int lineCount = 0;
            string line = lines[lineCount++];

            Dictionary<int, string> rawRules = new Dictionary<int, string>();
            while (!string.IsNullOrWhiteSpace(line))
            {
                var ruleParts = line.Split(": ");
                int ruleId = int.Parse(ruleParts[0]);
                rawRules.Add(ruleId, ruleParts[1]);
                line = lines[lineCount++];
            }

            Dictionary<int, List<string>> expandedRules = new Dictionary<int, List<string>>();
            List<string> ruleZero = ExpandRule(0, expandedRules, rawRules);

            int matchingLines = 0;
            for (; lineCount < lines.Length; lineCount++)
            {
                line = lines[lineCount];
                if (ruleZero.Any(z => z == line))
                {
                    matchingLines++;
                }
            }

            Console.WriteLine($"The number of matching lines is {matchingLines}.");
        }

        private static List<string> ExpandRule(int ruleId, Dictionary<int, List<string>> rules, Dictionary<int, string> rawRules)
        {
            List<string> expandedValue;

            if (rules.ContainsKey(ruleId))
            {
                expandedValue = rules[ruleId];
            }
            else
            {
                string rawRule = rawRules[ruleId];

                if (rawRule.Contains("\""))
                {
                    rules.Add(ruleId, new List<string> { rawRule.Replace("\"", "") });
                }
                else
                {
                    var orParts = rawRule.Split('|');
                    List<string> expandedValues = new List<string>();
                    foreach (string orPart in orParts)
                    {
                        var subRuleIds = orPart.Trim().Split(' ');
                        List<string> subRules = null;
                        foreach (string subRuleIdString in subRuleIds)
                        {
                            int subRuleId = int.Parse(subRuleIdString);
                            List<string> expandedSubRule = ExpandRule(subRuleId, rules, rawRules);
                            if (subRules == null)
                            {
                                subRules = expandedSubRule;
                            }
                            else
                            {
                                var query =
                                    from x in subRules
                                    from y in expandedSubRule
                                    select x + y;
                                subRules = query.ToList();
                            }
                        }
                        foreach (var subRule in subRules)
                        {
                            expandedValues.Add(subRule);
                        }
                    }
                    rules.Add(ruleId, expandedValues);
                }

                expandedValue = rules[ruleId];
            }
            return expandedValue;
        }
    }
}
