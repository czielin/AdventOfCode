using System;
using System.IO;
using System.Threading.Tasks;

namespace Day25
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync("input.txt");

            long cardPublicKey = long.Parse(lines[0]);
            long doorPublicKey = long.Parse(lines[1]);
            long subjectNumber = 7;
            long candidatePublicKey = subjectNumber;
            long cardLoopSize = 1;
            long doorLoopSize = 1;

            while (candidatePublicKey != cardPublicKey)
            {
                candidatePublicKey *= subjectNumber;
                candidatePublicKey = candidatePublicKey % 20201227;
                cardLoopSize++;
            }

            candidatePublicKey = subjectNumber;

            while (candidatePublicKey != doorPublicKey)
            {
                candidatePublicKey *= subjectNumber;
                candidatePublicKey = candidatePublicKey % 20201227;
                doorLoopSize++;
            }

            long privateKey = 1;
            for (int loopCount = 0; loopCount < cardLoopSize; loopCount++)
            {
                privateKey *= doorPublicKey;
                privateKey = privateKey % 20201227;
            }

            Console.WriteLine($"Card loop size: {cardLoopSize}");
            Console.WriteLine($"Door loop size: {doorLoopSize}");
            Console.WriteLine($"Private key: {privateKey}");
        }
    }
}
