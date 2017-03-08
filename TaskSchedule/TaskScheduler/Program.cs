using System.IO;
using Newtonsoft.Json;
using TaskSchedule;

namespace TaskScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = JsonConvert.DeserializeObject(File.ReadAllText("../../../tasks.txt"), typeof(Task[]));
            
        }
    }
}
