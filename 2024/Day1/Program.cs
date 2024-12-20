namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            int totalDifference = 0;
            int similarityScore = 0;
            var left = new int[lines.Length];
            var right = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split("   ");
                left[i] = int.Parse(parts[0]);
                right[i] = int.Parse(parts[1]);
            }
            var sortedLeft = left.Order();
            var sortedRight = right.Order();
            for (int i = 0; i < left.Length; i++)
            {
                totalDifference += Math.Abs(sortedRight.ElementAt(i) - sortedLeft.ElementAt(i));
                similarityScore += left[i] * right.Count(r => r == left[i]);
            }
            Console.WriteLine($"Total difference: {totalDifference}");
            Console.WriteLine($"Similarity score: {similarityScore}");
            Console.ReadLine();

        }
    }
}
