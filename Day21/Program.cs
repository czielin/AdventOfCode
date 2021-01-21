using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            List<Food> foods = new List<Food>();
            foreach (string line in lines)
            {
                var foodParts = line.Split(" (contains ");
                var ingredients = foodParts[0].Split(" ").ToList();
                var allergens = foodParts[1].TrimEnd(')').Split(", ").ToList();
                Food food = new Food { Ingredients = ingredients, Allergens = allergens };
                foods.Add(food);
            }

            var unidentifiedAllergens = foods.SelectMany(f => f.Allergens).Distinct().ToList();
            Dictionary<string, string> identifiedAllergens = new Dictionary<string, string>();

            while (unidentifiedAllergens.Any())
            {
                foreach (string allergen in unidentifiedAllergens)
                {
                    var foodsWithAllergen = foods.Where(f => f.Allergens.Contains(allergen));
                    var potentialIngredients = foodsWithAllergen.Select(f => f.Ingredients).Aggregate((a, b) => a.Intersect(b).ToList());
                    potentialIngredients.RemoveAll(i => identifiedAllergens.Values.Contains(i));
                    Console.WriteLine($"Potential ingredients for {allergen}: {string.Join(", ", potentialIngredients)}");
                    if (potentialIngredients.Count() == 1)
                    {
                        identifiedAllergens.Add(allergen, potentialIngredients.Single());
                    }
                }
                unidentifiedAllergens.RemoveAll(a => identifiedAllergens.ContainsKey(a));
            }

            int safeIngredientCount = foods.Sum(f => f.Ingredients.Count(i => !identifiedAllergens.Values.Contains(i)));

            Console.WriteLine($"The number of safe ingredients is {safeIngredientCount}");

            string dangerousIngredientList = string.Join(',', identifiedAllergens.OrderBy(i => i.Key).Select(i => i.Value));

            Console.WriteLine($"The dangerous ingredients are: {dangerousIngredientList}");
        }
    }
}
