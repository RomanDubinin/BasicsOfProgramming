using System;
using System.Collections.Generic;
using System.Linq;
using TaskSchedule;

namespace TaskScheduler
{
    public class TaskScheduler
    {
        public List<Tuple<int, int>> CreateSchedule(List<ExtendedTask> data)
        {
            var processorSchedule = new List<Tuple<int, int>>();
            var receivedTasks = new List<ExtendedTask>();

            var currentTick = 0;
            while (data.Count() != 0 || receivedTasks.Count != 0)
            {
                while (data.Count != 0 && data.First().Task.Start == currentTick)
                {
                    receivedTasks.Add(data.First());
                    data.RemoveAt(0);
                }

                if (receivedTasks.Count == 0)
                    continue;

                processorSchedule.Add(Tuple.Create(currentTick, receivedTasks.First().Id));
                receivedTasks.First().RemainingTime--;

                if (receivedTasks.First().RemainingTime == 0)
                    receivedTasks.RemoveAt(0);

                currentTick++;
            }
            return processorSchedule;
        }
    }
}