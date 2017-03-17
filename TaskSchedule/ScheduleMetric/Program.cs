using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TaskSchedule;

namespace ScheduleMetric
{
    class Program
    {
        static void Main(string[] args)
        {
            var schedule =
                (KeyValuePair<Task, int[]>[])
                JsonConvert.DeserializeObject(File.ReadAllText("../../../schedule2.txt"),
                    typeof(KeyValuePair<Task, int[]>[]));

            var relationOfDurationToActualTicks = new List<double>();
            var timeOfWaitingToStart = new List<double>();

            foreach (var pair in schedule)
            {
                relationOfDurationToActualTicks.Add(pair.Key.Duration / (double)(pair.Value.Last() - pair.Key.Start));
                timeOfWaitingToStart.Add(pair.Value.First() - pair.Key.Start);
            }
            Console.WriteLine("Duration / Actual ticks:");
            PrintStatistic(relationOfDurationToActualTicks);

            Console.WriteLine();
            Console.WriteLine("Time of waiting to start:");
            PrintStatistic(timeOfWaitingToStart);

        }

        private static void PrintStatistic(List<double> relationOfDurationToActualTicks)
        {
            Console.WriteLine("min: {0}", relationOfDurationToActualTicks.Min());
            Console.WriteLine("max: {0}", relationOfDurationToActualTicks.Max());
            Console.WriteLine("avg: {0}", relationOfDurationToActualTicks.Average());
            Console.WriteLine("median: {0}", relationOfDurationToActualTicks
                .OrderBy(x => x)
                .Skip(relationOfDurationToActualTicks.Count / 2)
                .First());
            Console.WriteLine("moda: {0}", relationOfDurationToActualTicks
                .GroupBy(x => Math.Round(x, 1))
                .OrderBy(g => g.Count())
                .First()
                .Key);
        }
    }
}
