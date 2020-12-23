using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            long sum = 0;
            long additionPrioritizedSum = 0;
            foreach (string line in lines)
            {
                var tokens = line.Replace(" ", "");
                int tokenCount = 0;
                sum += Evaluate(tokens, ref tokenCount);
                tokenCount = 0;
                additionPrioritizedSum += Evaluate(PrioritizeAddition(tokens), ref tokenCount);
            }

            Console.WriteLine($"The sum is {sum}.");
            Console.WriteLine($"The addition prioritized sum is {additionPrioritizedSum}.");
        }

        private static string PrioritizeAddition(string originalExpression)
        {
            string newExpression = "";
            List<int> pendingParens = new List<int>();
            for (int position = 0; position < originalExpression.Length; position++)
            {
                char currentToken = originalExpression[position];
                newExpression += currentToken;
                if (currentToken == '+')
                {
                    newExpression = InsertOpenParen(newExpression, newExpression.Length - 2);
                    pendingParens.Add(0);
                }
                else if (currentToken == '(')
                {
                    pendingParens = pendingParens.Select(p => p + 1).ToList();
                }
                else if (currentToken == ')')
                {
                    pendingParens = pendingParens.Select(p => p - 1).ToList();
                }
                if (currentToken != '+')
                {
                    for (int parenCount = 0; parenCount < pendingParens.Count(p => p == 0); parenCount++)
                    {
                        newExpression += ')';
                    }
                    pendingParens = pendingParens.Where(p => p != 0).ToList();
                }
            }
            return newExpression;
        }

        private static string InsertOpenParen(string originalExpression, int insertPosition)
        {
            int parenCount = 0;
            int currentPosition = insertPosition;
            while (parenCount > 0 || originalExpression[currentPosition] == ')')
            {
                char currentChar = originalExpression[currentPosition];
                if (currentChar == ')')
                {
                    parenCount++;
                }
                else if (currentChar == '(')
                {
                    parenCount--;
                }
                if (parenCount > 0 || currentChar != '(')
                {
                    currentPosition--;
                }
            }
            return originalExpression.Insert(currentPosition, "(");
        }

        private static long Evaluate(string tokens, ref int tokenCount)
        {
            long result;
            char firstToken = tokens[tokenCount++];
            if (firstToken == '(')
            {
                result = Evaluate(tokens, ref tokenCount);
            }
            else
            {
                result = long.Parse(firstToken.ToString());
            }

            while (tokenCount < tokens.Length)
            {
                char oper = tokens[tokenCount++];

                if (oper == ')')
                {
                    break;
                }

                char operand = tokens[tokenCount++];
                if (oper == '+')
                {
                    if (operand == '(')
                    {
                        result = result + Evaluate(tokens, ref tokenCount);
                    }
                    else
                    {
                        result = result + long.Parse(operand.ToString());
                    }
                }
                else if (oper == '*')
                {
                    if (operand == '(')
                    {
                        result = result * Evaluate(tokens, ref tokenCount);
                    }
                    else
                    {
                        result = result * long.Parse(operand.ToString());
                    }
                }
            }

            return result;
        }
    }
}
