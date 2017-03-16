
using System.Collections.Generic;
using System.Linq;

namespace TaskSchedule
{
    public class TaskScheduler
    {
        private int CurrentTick;
        
        public List<int?> CreateSchedule(List<Task> tasks)
        {
            var data = ExtendTaskData(tasks)
                .OrderBy(x => x.Task.Start)
                .ToList();

            var processorSchedule = new List<int?>();
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
                    processorSchedule.Add(null);
                    CurrentTick++;
                    continue;
                }

                var currentTask = GetMostPriorityExtendedTask(receivedTasks);

                processorSchedule.Add(currentTask.Id);
                currentTask.PassedTime++;

                if (currentTask.PassedTime == currentTask.Task.Duration)
                    receivedTasks.Remove(currentTask);

                CurrentTick++;
            }
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