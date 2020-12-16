using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            int lineIndex = 0;
            string line = lines[lineIndex++];
            List<Field> fields = new List<Field>();
            while (!string.IsNullOrWhiteSpace(line))
            {
                var fieldParts = line.Split(": ");
                Field field = new Field { Name = fieldParts[0] };
                var ranges = fieldParts[1].Split(" or ");
                var rangeParts = ranges[0].Split("-");
                field.Range1 = new Range { Start = int.Parse(rangeParts[0]), End = int.Parse(rangeParts[1]) };
                rangeParts = ranges[1].Split("-");
                field.Range2 = new Range { Start = int.Parse(rangeParts[0]), End = int.Parse(rangeParts[1]) };
                fields.Add(field);
                line = lines[lineIndex++];
            }

            lineIndex++;

            line = lines[lineIndex++];
            var myTicket = line.Split(",").Select(n => int.Parse(n));

            lineIndex += 2;

            List<IEnumerable<int>> nearbyTickets = new List<IEnumerable<int>>();
            for (; lineIndex < lines.Length; lineIndex++)
            {
                line = lines[lineIndex];
                nearbyTickets.Add(line.Split(",").Select(n => int.Parse(n)));
            }

            int errorRate = 0;
            List<IEnumerable<int>> validNearbyTickets = new List<IEnumerable<int>>();

            foreach (var ticket in nearbyTickets)
            {
                bool isValid = true;
                foreach (int fieldValue in ticket)
                {
                    if (!fields.Any(f => (fieldValue >= f.Range1.Start && fieldValue <= f.Range1.End) || (fieldValue >= f.Range2.Start && fieldValue <= f.Range2.End)))
                    {
                        errorRate += fieldValue;
                        isValid = false;
                    }
                }
                if (isValid)
                {
                    validNearbyTickets.Add(ticket);
                }
            }

            Console.WriteLine($"The error rate is {errorRate}.");

            Dictionary<int, List<string>> matchingFieldSets = new Dictionary<int, List<string>>();

            for (int index = 0; index < myTicket.Count(); index++)
            {
                var matchingFields =
                    fields.Where
                    (
                        f => validNearbyTickets.All
                        (
                            n =>
                            (n.ElementAt(index) >= f.Range1.Start && n.ElementAt(index) <= f.Range1.End)
                            || (n.ElementAt(index) >= f.Range2.Start && n.ElementAt(index) <= f.Range2.End)
                        )
                    );
                matchingFieldSets.Add(index, matchingFields.Select(m => m.Name).ToList());
            }

            Dictionary<string, int> myTicketFields = new Dictionary<string, int>();

            while (matchingFieldSets.Any())
            {
                var singleMatch = matchingFieldSets.First(m => m.Value.Count() == 1);
                myTicketFields.Add(singleMatch.Value.Single(), myTicket.ElementAt(singleMatch.Key));
                matchingFieldSets.Remove(singleMatch.Key);
                foreach (var matchingFieldSet in matchingFieldSets)
                {
                    matchingFieldSet.Value.Remove(singleMatch.Value.Single());
                }
            }

            long departureProduct = 1;

            foreach (var myTicketField in myTicketFields)
            {
                if (myTicketField.Key.StartsWith("departure"))
                {
                    departureProduct *= myTicketField.Value;
                }
            }

            Console.WriteLine($"The departure product is {departureProduct}.");
        }
    }
}
