using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TaskSchedule;

namespace TaskScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = (Task[])JsonConvert.DeserializeObject(File.ReadAllText("../../../tasks.txt"), typeof(Task[]));

            var scheduler = new TaskSchedule.TaskScheduler();
            var schedule = scheduler.CreateSchedule(new List<Task>(tasks));
            
            Console.WriteLine(string.Join(" ", schedule.Item1));

            var keyValuePairs = new List<KeyValuePair<Task, List<int>>>();
            foreach (var key in schedule.Item2.Keys)
            {
                keyValuePairs.Add(new KeyValuePair<Task, List<int>>(key, schedule.Item2[key]));
            }

            File.WriteAllText("../../../schedule1.txt", JsonConvert.SerializeObject(schedule.Item1, Formatting.Indented));
            File.WriteAllText("../../../schedule2.txt", JsonConvert.SerializeObject(keyValuePairs, Formatting.Indented));

            var tasks1 =
                (KeyValuePair<Task, int[]>[])
                JsonConvert.DeserializeObject(File.ReadAllText("../../../schedule2.txt"),
                    typeof(KeyValuePair<Task, int[]>[]));
        }
    }
}
