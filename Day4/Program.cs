using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{

    class Program
    {
        private static List<string> requiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        private static List<string> eyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        private static int passportsWithAllRequiredFields = 0;
        private static int passportsWithAllValidValues = 0;

        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            Dictionary<string, string> fieldsChecklist = GetNewChecklist();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    CheckPassport(fieldsChecklist);
                    fieldsChecklist = GetNewChecklist();
                }
                else
                {
                    var keyValuePairs = line.Split(' ');
                    foreach (string keyValuePair in keyValuePairs)
                    {
                        var keyValue = keyValuePair.Split(':');
                        string key = keyValue[0];
                        string value = keyValue[1];
                        fieldsChecklist[key] = value;
                    }
                }
            }

            CheckPassport(fieldsChecklist);

            Console.WriteLine($"Passports with all required fields: {passportsWithAllRequiredFields}");
            Console.WriteLine($"Passports with all valid values: {passportsWithAllValidValues}");
        }

        private static void CheckPassport(Dictionary<string, string> passportFields)
        {
            if (passportFields.Values.All(v => v != null))
            {
                passportsWithAllRequiredFields++;
                bool valuesValid = true;
                foreach (var field in passportFields)
                {
                    switch (field.Key)
                    {
                        case "byr":
                            valuesValid = valuesValid && field.Value.Length == 4 && int.TryParse(field.Value, out int byr) && byr >= 1920 && byr <= 2002;
                            break;
                        case "iyr":
                            valuesValid = valuesValid && field.Value.Length == 4 && int.TryParse(field.Value, out int iyr) && iyr >= 2010 && iyr <= 2020;
                            break;
                        case "eyr":
                            valuesValid = valuesValid && field.Value.Length == 4 && int.TryParse(field.Value, out int eyr) && eyr >= 2020 && eyr <= 2030;
                            break;
                        case "hgt":
                            int hgtNumber = 0;
                            valuesValid = valuesValid && int.TryParse(field.Value.Substring(0, field.Value.Length - 2), out hgtNumber);
                            if (valuesValid)
                            {
                                string hgtUnit = field.Value.Substring(field.Value.Length - 2, 2);
                                if (hgtUnit == "cm")
                                {
                                    valuesValid = valuesValid && hgtNumber >= 150 && hgtNumber <= 193;
                                }
                                else if (hgtUnit == "in")
                                {
                                    valuesValid = valuesValid && hgtNumber >= 59 && hgtNumber <= 76;
                                }
                                else
                                {
                                    valuesValid = false;
                                }
                            }
                            break;
                        case "hcl":
                            string hcl = field.Value;
                            valuesValid = valuesValid && hcl.Length == 7 && hcl[0] == '#' && hcl.Substring(1, 6).All(c => IsHexDigit(c));
                            break;
                        case "ecl":
                            valuesValid = valuesValid && eyeColors.Contains(field.Value);
                            break;
                        case "pid":
                            string pid = field.Value;
                            valuesValid = valuesValid && pid.Length == 9 && int.TryParse(pid, out int pidInt);
                            break;
                    }
                }

                if (valuesValid)
                {
                    passportsWithAllValidValues++;
                }
            }
        }

        private static bool IsHexDigit(char digit)
        {
            return (digit >= '0' && digit <= '9') || (digit >= 'a' && digit <= 'f');
        }

        private static Dictionary<string, string> GetNewChecklist()
        {
            Dictionary<string, string> fieldsChecklist = new Dictionary<string, string>();
            foreach (string field in requiredFields)
            {
                fieldsChecklist.Add(field, null);
            }
            return fieldsChecklist;
        }
    }
}
