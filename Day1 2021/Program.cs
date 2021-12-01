using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1_2021
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            int previousMeasurement = int.MaxValue;
            int twoMeasurementsAgo = int.MaxValue;
            int increases = 0;
            List<int> slidingMeasurements = new List<int>();

            for (int lineCount = 0; lineCount < lines.Length; lineCount++)
            {
                int currentMeasurement = int.Parse(lines[lineCount]);
                if (currentMeasurement > previousMeasurement)
                {
                    increases++;
                }
                if (twoMeasurementsAgo != int.MaxValue)
                {
                    slidingMeasurements.Add(currentMeasurement + previousMeasurement + twoMeasurementsAgo);
                }
                twoMeasurementsAgo = previousMeasurement;
                previousMeasurement = currentMeasurement;
            }

            Console.WriteLine($"The number of increases is: {increases}");
            increases = 0;

            for (int measurementCount = 0; measurementCount < slidingMeasurements.Count() - 1; measurementCount++)
            {
                if (slidingMeasurements.ElementAt(measurementCount + 1) > slidingMeasurements.ElementAt(measurementCount))
                {
                    increases++;
                }
            }

            Console.WriteLine($"The number of sliding increases is: {increases}");
        }
    }
}
