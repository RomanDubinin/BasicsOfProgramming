using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TaskSchedule;

namespace TaskGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasksCount = 20;
            var latestStart = 10;
            var expectationOfDuration = 6;
            var dispersionOfDuration = 2;

            var normalRandom = new NormallyDistributedRandomNumberGenerator(new Random());
            var uniformRandom = new Random();

            var tasks = Enumerable
                .Range(0, tasksCount)
                .Select(x => new Task
                {
                    Start = uniformRandom.Next(latestStart),
                    Duration = (int) normalRandom.GetNextDouble(expectationOfDuration, dispersionOfDuration)
                })
                .ToArray();

            File.WriteAllText("../../../tasks.txt", JsonConvert.SerializeObject(tasks, Formatting.Indented));
        }

        static void Sample()
        {
            var numOfSamples = 1000;
            var random = new NormallyDistributedRandomNumberGenerator(new Random());
            var values = Enumerable
                .Range(0, numOfSamples)
                .Select(x => random.GetNextDouble(100, 20))
                .GroupBy(x => (int)((int)x / 10) * 10)
                .OrderBy(x => x.Key)
                .ToList();


            foreach (var value in values)
            {
                Console.WriteLine(value.Key + " " + value.Count());
            }
        }
    }
}
