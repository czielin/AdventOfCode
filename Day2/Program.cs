using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Part1();
            await Part2();
        }

        private static async Task Part1()
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            int validPasswords = 0;

            foreach (string line in lines)
            {
                var parts = line.Split('-', ':', ' ');
                int minimumNumber = int.Parse(parts[0]);
                int maximumNumber = int.Parse(parts[1]);
                char characterToCheck = parts[2][0];
                string password = parts[4];
                int characterCount = password.Count(c => c == characterToCheck);
                if (characterCount >= minimumNumber && characterCount <= maximumNumber)
                {
                    validPasswords++;
                }
            }

            Console.WriteLine($"There are {validPasswords} valid passwords.");
        }

        private static async Task Part2()
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            int validPasswords = 0;

            foreach (string line in lines)
            {
                var parts = line.Split('-', ':', ' ');
                int minimumNumber = int.Parse(parts[0]);
                int maximumNumber = int.Parse(parts[1]);
                char characterToCheck = parts[2][0];
                string password = parts[4];
                //int characterCount = password.Count(c => c == characterToCheck);
                if (password[minimumNumber - 1] == characterToCheck ^ password[maximumNumber - 1] == characterToCheck)
                {
                    validPasswords++;
                }
            }

            Console.WriteLine($"There are {validPasswords} valid passwords.");
        }
    }
}
