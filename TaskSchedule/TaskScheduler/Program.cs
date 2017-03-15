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
            
            Console.WriteLine(string.Join(" ", schedule));
        }
    }
}
