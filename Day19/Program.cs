using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            Dictionary<int, HashSet<string>> expandedRules = new Dictionary<int, HashSet<string>>();
            HashSet<string> ruleZero = ExpandRule(0, expandedRules, rawRules);

            int matchingLines = 0;
            int messagesStartLine = lineCount;
            for (; lineCount < lines.Length; lineCount++)
            {
                line = lines[lineCount];
                if (ruleZero.Any(z => z == line))
                {
                    matchingLines++;
                }
            }

            Console.WriteLine($"The number of matching lines is {matchingLines}.");

            Dictionary<int, string> expandedRulesPart2 = new Dictionary<int, string>();
            string ruleZeroPart2 = $"^{ExpandRuleRegex(0, expandedRulesPart2, rawRules)}$";

            matchingLines = 0;
            lineCount = messagesStartLine;
            for (; lineCount < lines.Length; lineCount++)
            {
                line = lines[lineCount];
                if (Regex.IsMatch(line, ruleZeroPart2))
                {
                    matchingLines++;
                }
            }

            Console.WriteLine($"The number of matching lines is {matchingLines}.");
        }

        private static string ExpandRuleRegex(int ruleId, Dictionary<int, string> rules, Dictionary<int, string> rawRules, int depth = 0)
        {
            string expandedValue;

            if (rules.ContainsKey(ruleId))
            {
                expandedValue = rules[ruleId];
            }
            else
            {
                string rawRule = rawRules[ruleId];

                if (rawRule.Contains("\""))
                {
                    rules.Add(ruleId, rawRule.Replace("\"", ""));
                }
                else
                {
                    var orParts = rawRule.Split('|');
                    HashSet<string> expandedValues = new HashSet<string>();
                    foreach (string orPart in orParts)
                    {
                        var subRuleIds = orPart.Trim().Split(' ');
                        string subRules = "";
                        foreach (string subRuleIdString in subRuleIds)
                        {
                            int subRuleId = int.Parse(subRuleIdString);
                            if (subRuleId == 8)
                            {
                                string fortyTwo = ExpandRuleRegex(42, rules, rawRules, depth + 1);
                                subRules += $"({fortyTwo})+";
                            }
                            else if (subRuleId == 11)
                            {
                                var fortyTwo = ExpandRuleRegex(42, rules, rawRules, depth + 1);
                                var thirtyOne = ExpandRuleRegex(31, rules, rawRules, depth + 1);
                                subRules += $"(({fortyTwo})({thirtyOne})|({fortyTwo}){{2}}({thirtyOne}){{2}}|({fortyTwo}){{3}}({thirtyOne}){{3}}|({fortyTwo}){{4}}({thirtyOne}){{4}}|({fortyTwo}){{5}}({thirtyOne}){{5}}|({fortyTwo}){{6}}({thirtyOne}){{6}})";
                            }
                            else
                            {
                                subRules += $"({(ExpandRuleRegex(subRuleId, rules, rawRules, depth + 1))})";
                            }
                        }
                        expandedValues.Add(subRules);
                    }
                    rules.Add(ruleId, string.Join('|', expandedValues));
                }

                expandedValue = rules[ruleId];
            }

            return expandedValue;
        }

        private static HashSet<string> ExpandRule(int ruleId, Dictionary<int, HashSet<string>> rules, Dictionary<int, string> rawRules, int depth = 0)
        {
            HashSet<string> expandedValue;

            if (rules.ContainsKey(ruleId))
            {
                expandedValue = rules[ruleId];
            }
            else
            {
                string rawRule = rawRules[ruleId];

                if (rawRule.Contains("\""))
                {
                    rules.Add(ruleId, new HashSet<string> { rawRule.Replace("\"", "") });
                }
                else
                {
                    var orParts = rawRule.Split('|');
                    HashSet<string> expandedValues = new HashSet<string>();
                    foreach (string orPart in orParts)
                    {
                        var subRuleIds = orPart.Trim().Split(' ');
                        HashSet<string> subRules = null;
                        foreach (string subRuleIdString in subRuleIds)
                        {
                            int subRuleId = int.Parse(subRuleIdString);
                            HashSet<string> expandedSubRule;

                            expandedSubRule = ExpandRule(subRuleId, rules, rawRules, depth + 1);

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
                                subRules = query.ToHashSet();
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

        private static HashSet<string> CombineSelf(HashSet<string> list, int times)
        {
            if (times == 1)
            {
                return list;
            }
            else
            {
                HashSet<string> combinations = new HashSet<string>();
                combinations.UnionWith(list);
                combinations.UnionWith(Combine(list, CombineSelf(list, times - 1)));
                return combinations;
            }
        }

        private static HashSet<string> CombineSandwich(HashSet<string> list1, HashSet<string> list2, int times)
        {
            if (times == 1)
            {
                return Combine(list1, list2);
            }
            else
            {
                HashSet<string> combinations = Combine(list1, list2);
                combinations.UnionWith(Combine(list1, Combine(CombineSandwich(list1, list2, times - 1), list2)));
                return combinations;
            }
        }

        private static HashSet<string> Combine(HashSet<string> list1, HashSet<string> list2)
        {
            var query =
                from x in list1
                from y in list2
                select x + y;
            return query.ToHashSet();
        }
    }
}
