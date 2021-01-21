using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day22
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            List<int> player1InitialDeck = new List<int>();
            List<int> player2InitialDeck = new List<int>();

            int player2Start = Array.IndexOf(lines, "Player 2:");

            for (int count = 0; count < player2Start; count++)
            {
                string line = lines[count];
                if (int.TryParse(line, out int cardValue))
                {
                    player1InitialDeck.Add(cardValue);
                }
            }

            for (int count = player2Start; count < lines.Length; count++)
            {
                string line = lines[count];
                if (int.TryParse(line, out int cardValue))
                {
                    player2InitialDeck.Add(cardValue);
                }
            }

            List<int> player1 = player1InitialDeck.ToList();
            List<int> player2 = player2InitialDeck.ToList();

            while (player1.Any() && player2.Any())
            {
                int player1Card = player1.First();
                player1.RemoveAt(0);
                int player2Card = player2.First();
                player2.RemoveAt(0);

                if (player1Card > player2Card)
                {
                    player1.Add(player1Card);
                    player1.Add(player2Card);
                }
                else if (player2Card > player1Card)
                {
                    player2.Add(player2Card);
                    player2.Add(player1Card);
                }
            }

            List<int> winningHand = player1.Any() ? player1 : player2;

            Console.WriteLine($"The winning score is: {Score(winningHand)}");

            var result = RecursiveCombat(player1InitialDeck, player2InitialDeck);

            Console.WriteLine($"The winning recursive combat score is: {Score(result.WinningHand)}");
        }

        private static (int WinningPlayer, List<int> WinningHand) RecursiveCombat(List<int> player1, List<int> player2)
        {
            HashSet<string> previousRounds = new HashSet<string>();
            int round = 1;
            //(int WinningPlayer, List<int> WinningHand) result = (0, null);
            int winner = 0;
            while (player1.Any() && player2.Any())
            {
                string roundKey = string.Join(',', player1) + "|" + string.Join(',', player2);
                //Console.WriteLine($"Round {round}");
                //Console.WriteLine($"Player 1 deck: {string.Join(',', player1)}");
                //Console.WriteLine($"Player 2 deck: {string.Join(',', player2)}");
                int player1Card = player1.First();
                player1.RemoveAt(0);
                int player2Card = player2.First();
                player2.RemoveAt(0);

                //Console.WriteLine($"Player 1 plays: {player1Card}");
                //Console.WriteLine($"Player 2 plays: {player2Card}");

                if (previousRounds.Contains(roundKey))
                {
                    winner = 1;
                }
                else if (player1Card <= player1.Count() && player2Card <= player2.Count())
                {
                    //Console.WriteLine("Starting sub game.");
                    winner = RecursiveCombat(player1.Take(player1Card).ToList(), player2.Take(player2Card).ToList()).WinningPlayer;
                }
                else
                {
                    if (player1Card > player2Card)
                    {
                        winner = 1;
                    }
                    else if (player2Card > player1Card)
                    {
                        winner = 2;
                    }
                }

                if (winner == 1)
                {
                    player1.Add(player1Card);
                    player1.Add(player2Card);
                }
                else
                {
                    player2.Add(player2Card);
                    player2.Add(player1Card);
                }

                //Console.WriteLine($"Player {winner} wins.");
                //Console.WriteLine();

                round++;
                previousRounds.Add(roundKey);
            }

            return (winner, winner == 1 ? player1 : player2);
        }

        private static long Score(List<int> hand)
        {
            long score = 0;

            for (int index = 0; index < hand.Count(); index++)
            {
                int cardValue = hand[index];
                score += cardValue * (hand.Count() - index);
            }

            return score;
        }
    }
}
