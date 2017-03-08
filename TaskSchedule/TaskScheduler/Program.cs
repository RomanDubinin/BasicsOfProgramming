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
            var data = tasks
                .Select((task, i) => new ExtendedTask
                {
                    Task = task,
                    Id = i,
                    RemainingTime = task.Duration

                })
                .OrderBy(x => x.Task.Start)
                .ToList();

            var scheduler = new TaskScheduler();
            var chedule = scheduler.CreateSchedule(data);
        }
    }
}
