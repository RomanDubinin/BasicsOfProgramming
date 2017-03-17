
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskSchedule
{
    public class TaskScheduler
    {
        private int CurrentTick;
        
        public Tuple<List<int>, Dictionary<Task, List<int>>> CreateSchedule(List<Task> tasks)
        {
            var data = ExtendTaskData(tasks)
                .OrderBy(x => x.Task.Start)
                .ToList();

            var processorSchedule = new List<int>();
            var processorSchedule2 = InitializeShedule(data);

            var receivedTasks = new List<ExtendedTask>();

            CurrentTick = 0;
            while (data.Count != 0 || receivedTasks.Count != 0)
            {
                while (data.Count != 0 && data.First().Task.Start == CurrentTick)
                {
                    receivedTasks.Add(data.First());
                    data.RemoveAt(0);
                }

                if (receivedTasks.Count == 0)
                {
                    processorSchedule.Add(-1);
                    CurrentTick++;
                    continue;
                }

                var currentTask = GetMostPriorityExtendedTask(receivedTasks);

                processorSchedule.Add(currentTask.Id);
                processorSchedule2[currentTask.Task].Add(CurrentTick);
                currentTask.PassedTime++;

                if (currentTask.PassedTime == currentTask.Task.Duration)
                    receivedTasks.Remove(currentTask);

                CurrentTick++;
            }
            return Tuple.Create(processorSchedule, processorSchedule2);
        }

        private Dictionary<Task, List<int>> InitializeShedule(List<ExtendedTask> extendedTasks)
        {
            var processorSchedule = new Dictionary<Task, List<int>>();

            foreach (var extendedTask in extendedTasks)
                processorSchedule.Add(extendedTask.Task, new List<int>());

            return processorSchedule;
        }

        private List<ExtendedTask> ExtendTaskData(List<Task> tasks)
        {
            return tasks
                .Select((task, i) => new ExtendedTask
                {
                    Task = task,
                    Id = i,
                    PassedTime = 0

                })
                .ToList();
        }

        private ExtendedTask GetMostPriorityExtendedTask(List<ExtendedTask> tasks)
        {
            return tasks.Aggregate((curMin, x) =>
                PercentOfProcessTime(curMin) <= PercentOfProcessTime(x) ? curMin : x);
        }

        private double PercentOfProcessTime(ExtendedTask extendedTask)
        {
            return (double)(extendedTask.PassedTime) / (CurrentTick - extendedTask.Task.Start);
        }
    }
}